using AutoMapper;
using System;
using managing_humanitarian_collections_api.Authorization;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Exceptions;
using managing_humanitarian_collections_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace managing_humanitarian_collections_api.Services
{
    #region Intefejsy
    public interface IOrderService
    {
        int CreateOrder(CreateOrderDto dto);

        int AddProductsToOrder(int collectionProductId, AddProductToOrderDto dto);

        void UpdateOrderStatus(int id, UpdateOrderStatusDto dto);

        IEnumerable<OrderDto> GetAll();

        OrderDto GetById(int id);

        IEnumerable<OrdersPerDonator> GetOrdersPerDonator(int id);
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

        public int CreateOrder(CreateOrderDto dto)
        {
            var order = _mapper.Map<Order>(dto);
            order.CreatedByDonatorId = _userContextService.GetUserId;
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            return order.Id;
        }

       

        public IEnumerable<OrderDto> GetAll()
        {
            var orders = _dbContext
                .Orders
                //.Include(r => r.Product)
                //.ThenInclude(n => n.Address)
                .ToList();

           


            if (orders is null) throw new NotFoundException("Nie znaleziono zamówienia");

            var orderDtos = _mapper.Map<List<OrderDto>>(orders);

            return orderDtos;
        }

        public OrderDto GetById(int id)
        {
            var order = _dbContext.Orders
                .Include(n => n.OrderProducts)
               // .ThenInclude(n => n.Product)
                .FirstOrDefault(r => r.Id == id);

            if (order is null) throw new NotFoundException("Nie znaleziono zamówienia");

            var result = _mapper.Map<OrderDto>(order);
            return result;
        }

        public int AddProductsToOrder(int orderId, AddProductToOrderDto dto)
        {
            var orderProduct = _mapper.Map<OrderProduct>(dto);

            orderProduct.OrderId = orderId;

            _dbContext.OrderProducts.Add(orderProduct);           

            var newQuantily = _dbContext.CollectionProducts
                 .Where(r => r.ProductId == dto.ProductId)
                 .FirstOrDefault(r => r.Id == dto.CollectionProductId);
            newQuantily.Quantily = newQuantily.Quantily - dto.Quantily;

            _dbContext.SaveChanges();

            return orderProduct.Id;

        }

        public void UpdateOrderStatus(int id, UpdateOrderStatusDto dto)
        {
            var order = _dbContext
                     .Orders
                     .Include(r => r.OrderProducts)
                     .FirstOrDefault(r => r.Id == id);

            if (order is null)
                throw new NotFoundException("Nie znaleziono zamówienia");

            order.DeliveryStatus = dto.DeliveryStatus;

            var orderProduct = _dbContext
                 .OrderProducts
                 .Where(r => r.OrderId == id)
                 .ToList();

            var collectionProduct = _dbContext
                .CollectionProducts  
                .ToList();

            if(dto.DeliveryStatus == "failed")
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
                .Where(r => r.CreatedByDonatorId == id)
                .ToList();

            if (orders is null) throw new NotFoundException("Nie znaleziono zamówienia");

            var orderDtos = _mapper.Map<List<OrdersPerDonator>>(orders);

            return orderDtos;
        }

    }
}
