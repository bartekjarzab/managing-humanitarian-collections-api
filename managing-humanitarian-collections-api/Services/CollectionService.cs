using AutoMapper;
using managing_humanitarian_collections_api.Authorization;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Exceptions;
using managing_humanitarian_collections_api.Models;
using managing_humanitarian_collections_api.Models.Collection;

using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace managing_humanitarian_collections_api.Services
{
    #region Intefejsy
    public interface ICollectionService
    {
        int CreateCollection(CreateCollectionDto dto);
        CollectionWithAddressDto GetCollectionWithAddressById(int id);
        void UpdateCollectionStatus(int id, UpdateCollectionStatusDto dto);
        IEnumerable<CollectionWithAddressDto> GetAll();
        void Delete(int id);
        List<CollectionProductsListDto> GetAllProducts(int collectionId);
        int CreateCollectionNeededProducts(int collectionId, CreateCollectionProductDto dto);
        List<CollectionDto> GetAllCollections();
        CollectionDto GetCollection(int id);


    }

    #endregion
    public class CollectionService : ICollectionService
    {
        private readonly ManagingCollectionsDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public CollectionService(ManagingCollectionsDbContext dbContext, IMapper mapper, ILogger<CollectionService> logger, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        #region Tworzenie zbiórki
        public int CreateCollection(CreateCollectionDto dto)
        {
            var collection = _mapper.Map<Collection>(dto);
            collection.CreatedByOrganiserId = _userContextService.GetUserId;
            _dbContext.Collections.Add(collection);
            _dbContext.SaveChanges();

            return collection.Id;
        }
        #endregion
        #region Wszystkie zbiórki
        public List<CollectionDto> GetAllCollections()
        {
            var collections = _dbContext
                .Collections
                .ToList();

            var collectionsDtoS = _mapper.Map<List<CollectionDto>>(collections);

            return collectionsDtoS;
        }

        public CollectionDto GetCollection(int id)
        {
            var collection = _dbContext
                .Collections
                .FirstOrDefault(r => r.Id == id);

            var collectionDto = _mapper.Map<CollectionDto>(collection);

            return collectionDto;
        }
        #endregion
        #region Zmiana statusu zbiórki
        public void UpdateCollectionStatus(int id, UpdateCollectionStatusDto dto)
        {
            var collection = _dbContext
                .Collections
                .FirstOrDefault(r => r.Id == id);

            if (collection is null)
                throw new NotFoundException("Nie znaleziono zbiórki");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, collection,
               new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            collection.Status = dto.Status;

            _dbContext.SaveChanges();
        }
        #endregion
        #region Usunięcie zbiórki
        public void Delete(int id)
        {
            _logger.LogError($"Zbiórka o Id {id} została usunięta");

            var collection = _dbContext
                .Collections
                .FirstOrDefault(r => r.Id == id);

            if (collection is null)
                throw new NotFoundException("Nie znaleziono zbiórki");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, collection,
                new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            _dbContext.Collections.Remove(collection);
            _dbContext.SaveChanges();
        }
        #endregion
        #region Lista wszystkich zbiórek z adresem
        public IEnumerable<CollectionWithAddressDto> GetAll()
        {
            var collections = _dbContext
                .Collections
                .Include(r => r.CollectionPoints)
                .ThenInclude(n => n.Address)
                .ToList();

            if (collections is null) throw new NotFoundException("Collection not found");

            var collectionDtos = _mapper.Map<List<CollectionWithAddressDto>>(collections);

            return collectionDtos;
        }
        #endregion
        #region Zbiórka z adresem po id
        public CollectionWithAddressDto GetCollectionWithAddressById(int id)
        {
            var collection = _dbContext
                .Collections
                .Include(n => n.CollectionPoints)
                .ThenInclude(n => n.Address)
                .FirstOrDefault(r => r.Id == id);
 
            if (collection is null) throw new NotFoundException("Collection not found");

            var result = _mapper.Map<CollectionWithAddressDto>(collection);
            return result;
        }
        #endregion
        #region Lista produktów potrzebnych w zbiórce

        public List<CollectionProductsListDto> GetAllProducts(int collectionId)
        {
            var collection = _dbContext
                .Collections
                .Include(a => a.CollectionProducts)
                .ThenInclude(a => a.Product)
                .FirstOrDefault(r => r.Id == collectionId);

            var collectionProductsDtos = _mapper.Map<List<CollectionProductsListDto>>(collection.CollectionProducts);

            return collectionProductsDtos;
        }
        #endregion
        #region Dodanie potrzebnych przedmiotów do zbiórki
        public int CreateCollectionNeededProducts(int collectionId, CreateCollectionProductDto dto)
        {           
            var collectionProduct = _mapper.Map<CollectionProduct>(dto);

            collectionProduct.CollectionId = collectionId;

            _dbContext.CollectionProducts.Add(collectionProduct);
            _dbContext.SaveChanges();

            return collectionProduct.Id;
        }
        #endregion
    }
}
