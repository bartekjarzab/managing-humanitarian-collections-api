using AutoMapper;
using managing_humanitarian_collections_api.Authorization;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Exceptions;
using managing_humanitarian_collections_api.Models;
using managing_humanitarian_collections_api.Models.QueryValidators;
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
        int CreateOrder(CreateOrder dto);
        CollectionWithAddressDto GetById(int id);
        void UpdateCollectionStatus(int id, UpdateCollectionStatusDto dto);
        IEnumerable<CollectionWithAddressDto> GetAll();
        IEnumerable<CollectionWithProductsDto> GetCollectionWithProducts(int id);
        void Delete(int id);
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

        #region Tworzenie koszyka
        public int CreateOrder(CreateOrder dto)
        {
            var order = _mapper.Map<Order>(dto);
            order.DonatorId = _userContextService.GetUserId;
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            return order.Id;
        }
        public void SetQuantilyProductForOrder(QuantilyProductsForOrderDto dto, int id)
        {
            var orderProduct = _dbContext
                .OrderProducts
                .FirstOrDefault(r => r.Id == id);


            if (orderProduct is null) throw new NotFoundException("nie znaleziono listy");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, orderProduct,
             new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }
            //do sprawdzenia po 
            orderProduct.Quantily = (int)dto.Quantity;

            _dbContext.SaveChanges();
        }

      
        #endregion
        #region zmiana statusu zbiórki
        //public void UpdateCollectionStatus(int id, UpdateCollectionStatusDto dto)
        //{
        //    var collection = _dbContext
        //        .Collections
        //        .FirstOrDefault(r => r.Id == id);

        //    if (collection is null)
        //        throw new NotFoundException("Nie znaleziono zbiórki");

        //    var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, collection,
        //       new ResourceOperationRequirement(ResourceOperation.Update)).Result;

        //    if (!authorizationResult.Succeeded)
        //    {
        //        throw new ForbidException();
        //    }

        //    collection.Status = dto.Status;

        //    _dbContext.SaveChanges();
        //}
        #endregion


        //do przemyslenia czy tylko tworzacy koszyk moze go usunac
        public void DeleteOrder(int id)
        {
            _logger.LogError($"Zamówienie o Id {id} została usunięta");

            var order = _dbContext
                .Orders
                .FirstOrDefault(r => r.Id == id);

            if (order is null)
                throw new NotFoundException("Nie znaleziono zamówienia");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, order,
                new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            _dbContext.Orders.Remove(order);
            _dbContext.SaveChanges();

        }
        #region Lista wszystkich zbiórek z adresem
        //public IEnumerable<CollectionWithAddressDto> GetAll()
        //{
        //    var collections = _dbContext
        //        .Collections
        //        .Include(r => r.CollectionPoints)
        //        .ThenInclude(n => n.Address)
        //        .ToList();

        //    if (collections is null) throw new NotFoundException("Collection not found");

        //    var collectionDtos = _mapper.Map<List<CollectionWithAddressDto>>(collections);

        //    return collectionDtos;
        //}
        #endregion

        #region Zbiórka z adresem po id
        //public CollectionWithAddressDto GetById(int id)
        //{
        //    var order = _dbContext.Orders
        //        .Include(n => n.OrderProducts)
        //        .ThenInclude(n => n.CollectionProduct)
        //        .FirstOrDefault(r => r.Id == id);
 
        //    if (order is null) throw new NotFoundException("order not found");

        //    var result = _mapper.Map<OrderWithCollectionDto>(Order);
        //    return result;
        //}
        #endregion

        #region Lista produktów potrzebnych w zbiórce

        //public IEnumerable<CollectionWithProductsDto> GetCollectionWithProducts(int id)
        //{
        //    var collection = _dbContext.Collections
        //        .Include(o => o.CollectionProducts)
        //        .ThenInclude(o => o.Product)
        //        .Where(r => r.Id == id)
        //        .ToList();

        //    if (collection is null) throw new NotFoundException("Collection not found");

        //    var result = _mapper.Map<List<CollectionWithProductsDto>>(collection);
        //        return result;
        //}
        #endregion

    }
}
