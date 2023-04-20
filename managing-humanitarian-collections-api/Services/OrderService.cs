using AutoMapper;
using System;
using managing_humanitarian_collections_api.Authorization;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using managing_humanitarian_collections_api.Models.Order;

namespace managing_humanitarian_collections_api.Services
{
    #region Intefejsy
    public interface IOrderService
    {
        int CreateOrder(int collectionId, CreateOrderDto dto);

        int AddProductsToOrder(int orderId, AddProductToOrderDto dto);

        void UpdateOrderStatus(int orderId, UpdateOrderStatusDto dto);

        IEnumerable<OrderDto> GetAllDonatorOrders(int userId);

        IEnumerable<OrderDto> GetAllCollectionOrders(int collectionId);
        OrderDto GetById(int id);
         
        IEnumerable<OrdersPerDonator> GetOrdersPerDonator(int id);
        IEnumerable<OrderProductListDto> GetProductsPerOrder(int id);

        void DeleteProductFromOrder(int orderId, int orderProductId);
    }

    #endregion
    public class OrderService : IOrderService
    {
        private readonly ManagingCollectionsDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public OrderService(ManagingCollectionsDbContext dbContext, IMapper mapper, ILogger<CollectionService> logger, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }
        #region Utworzenie zamówienie
        public int CreateOrder(int collectionId, CreateOrderDto dto)
        {   
            var orderEntity = _mapper.Map<Order>(dto);
            orderEntity.CreatedById = _userContextService.GetUserId;
            orderEntity.CreatedOrderDate = DateTime.Now.ToString("dd/MM/yyyy, HH:mm");
            orderEntity.OrderStatusId = 3;
            orderEntity.CollectionId = collectionId;

            var orderIsProgress = _dbContext
                .Orders
                .Where(x => x.CollectionId == collectionId)
                .Where(x => x.OrderStatusId == 3)
                .FirstOrDefault(x => x.CreatedById == (_userContextService.GetUserId));

            if (orderIsProgress != null)
                throw new BadRequestException("Masz już utworzone zamówienie dla tej zbiórki");

            _dbContext.Orders.Add(orderEntity);
            _dbContext.SaveChanges();

         

            return orderEntity.Id;
        }
        #endregion




        #region Dodanie produktów do zamówienia oraz odjęcie ilości zapotrzebowania
        public int AddProductsToOrder(int orderId, AddProductToOrderDto dto)
        {
            var order = _dbContext
                .OrderProducts
                .Where(p => p.OrderId == orderId)
                .ToList();

          //  var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, order,
          //new ResourceOperationRequirement(ResourceOperation.Create)).Result;

          //  if (!authorizationResult.Succeeded)
          //  {
          //      throw new ForbidException("Nie masz uprawnień do dodania przedmiotu");
          //  }

            var orderProduct = _mapper.Map<OrderProduct>(dto);
            orderProduct.OrderId = orderId;

            var newQuantily = _dbContext.CollectionProducts
                 .Where(r => r.ProductId == dto.ProductId)
                 .FirstOrDefault(r => r.Id == dto.CollectionProductId);
            foreach(var product in order)
            if (product.ProductId == dto.ProductId)
                throw new BadRequestException("Przedmiot został już dodany do zamówienia");
            if (newQuantily != null)
                if (newQuantily.Quantily < orderProduct.Quantily)
                    throw new BadRequestException("Zbiórka nie potrzebuje tyle przedmiotów");
            _dbContext.OrderProducts.Add(orderProduct);

            newQuantily.Quantily = newQuantily.Quantily - dto.Quantily;

            _dbContext.SaveChanges();

            return orderProduct.Id;

        }
        #endregion

        #region usunięcie przedmiotu z koszyka oraz aktualizacja stanu zapotrzebowania dla zbiórki

        public void DeleteProductFromOrder(int orderId, int orderProductId)
        {
            var orderProducts = _dbContext
                .Orders
                .Include(x => x.OrderProducts)
                .FirstOrDefault(p => p.Id == orderId);


                var collectionProducts = _dbContext
                .CollectionProducts
                .Where(x => x.CollectionId == orderProducts.CollectionId)
                .ToList();

            var ordProduct = _dbContext
               .OrderProducts
               .FirstOrDefault(x => x.Id == orderProductId);

            foreach (var product in collectionProducts)
               if(product.ProductId == ordProduct.ProductId)
                    product.Quantily = product.Quantily + ordProduct.Quantily;

           

            _dbContext.OrderProducts.Remove(ordProduct);
            _dbContext.SaveChanges();

        }

        #endregion


        #region Wszystkie zamówienia użytkownika
        public IEnumerable<OrderDto> GetAllDonatorOrders(int id)
        {
            var orders = _dbContext
                .Orders
                .Include(r => r.CreatedBy)
                .ThenInclude(r => r.Profile)
                .Include(r => r.OrderStatus)
                .Where(r => r.CreatedById == id)
                .OrderByDescending(x => x.OrderStatus)
                .ThenByDescending(x => x.CreatedOrderDate)
                .ToList();

            if (orders is null) throw new NotFoundException("Nie znaleziono zamówienia");

            var orderDtos = _mapper.Map<List<OrderDto>>(orders);

            return orderDtos;
        }
        #endregion

        #region Wszystkie zamówienia dla zbiórki
        public IEnumerable<OrderDto> GetAllCollectionOrders(int id)
        {
            var orders = _dbContext
                .Orders
                .Include(r => r.CreatedBy)
                .ThenInclude(r => r.Profile)
                .Include(r => r.OrderStatus)
                .Where(r => r.CollectionId == id)
                 .OrderByDescending(x => x.OrderStatus)
                .ThenByDescending(x => x.CreatedOrderDate)
                .ToList();

            if (orders is null) throw new NotFoundException("Nie znaleziono zamówienia");

            var orderDtos = _mapper.Map<List<OrderDto>>(orders);

            return orderDtos;


        }
        #endregion

        public OrderDto GetById(int id)
        {
            var order = _dbContext.Orders
                .Include(n => n.OrderProducts)
                .ThenInclude(n => n.Product)
                .FirstOrDefault(r => r.Id == id);

            if (order is null) throw new NotFoundException("Nie znaleziono zamówienia");

            var result = _mapper.Map<OrderDto>(order);
            return result;
        }

        public IEnumerable<OrderProductListDto> GetProductsPerOrder(int id)
        {
            var orderProducts = _dbContext
                .OrderProducts
                .Include(r => r.Product)
                .ThenInclude(r => r.Properties)
                .Where(r => r.OrderId == id)
                .ToList();

            if (orderProducts == null)
                throw new NotFoundException("nie znaleziono produktów dla tego zamówienia");

            var orderProductDtos = _mapper.Map<List<OrderProductListDto>>(orderProducts);

            return orderProductDtos;

        }

        public void UpdateOrderStatus(int id, UpdateOrderStatusDto dto)
        {
            var order = _dbContext
                     .Orders
                     .Include(r => r.OrderProducts)
                     .FirstOrDefault(r => r.Id == id);

            if (order is null)
                throw new NotFoundException("Nie znaleziono zamówienia");

            //var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, order,
            //new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            //if (!authorizationResult.Succeeded)
            //{
            //    throw new ForbidException("Brak uprawnień");
            //}

            order.OrderStatusId = dto.OrderStatusId;
            order.CreatedOrderDate = DateTime.Now.ToString("dd/MM/yyyy, HH:mm");

            var orderProduct = _dbContext
                 .OrderProducts
                 .Where(r => r.OrderId == id)
                 .ToList();

            var collectionProduct = _dbContext
                .CollectionProducts  
                .ToList();

            if(dto.OrderStatusId == 2)
            {
                foreach (var product in collectionProduct)
                    foreach (var item in orderProduct)
                        if (product.ProductId == item.ProductId)
                        product.Quantily = item.Quantily + product.Quantily;
            }
            _dbContext.SaveChanges();
        }
        public IEnumerable<OrdersPerDonator> GetOrdersPerDonator(int id)
        {
            var orders = _dbContext
                .Orders
                .Where(r => r.CreatedById == id)
                .ToList();

            if (orders is null) throw new NotFoundException("Nie znaleziono zamówienia");

            var orderDtos = _mapper.Map<List<OrdersPerDonator>>(orders);

            return orderDtos;
        }

        private Collection GetCollectionById(int collectionId)
        {
            var collection = _dbContext
                .Collections
                .Include(r =>r.Orders).
                FirstOrDefault(r => r.Id == collectionId);
            if (collection == null)
                throw new NotFoundException("Zbiórka nie znaleziona");

            return collection;
        }

    }
}
