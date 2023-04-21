using AutoMapper;
using System;
using managing_humanitarian_collections_api.Authorization;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Exceptions;
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
        void UpdateCollectionStatus(int id, UpdateCollectionStatusDto dto);
        IEnumerable<CollectionPointDto> GetAll();
        void Delete(int id);
        List<CollectionProductsListDto> GetAllProducts(int collectionId);
        int CreateCollectionNeededProducts(int collectionId, CreateCollectionProductDto dto);
        List<CollectionDto> GetAllCollections();
        CollectionDto GetCollection(int id);
        IEnumerable<CollectionDto> GetCollectionPerOrganiser(int id);
        void EditCollection(int collectionId, EditCollectionDto dto);
        void DeleteCollectionproduct(int id, int collectionId);
        void EditCollectionProduct(int collectionProductId, int collectionId, CreateCollectionProductDto dto);
        CollectionProductsListDto GetCollectionProductById(int collectionId, int collectionProductId);

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
            collection.CreateDate = DateTime.Now.ToString("dd/MM/yyyy, HH:mm");
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
                .Include(r => r.CreatedByOrganiser)
                .ThenInclude(r => r.Profile)
                .Include(r => r.CollectionStatus)
                .OrderByDescending(x => x.CollectionStatus)
                .OrderByDescending(x => x.Id)
                .ToList();

            if (collections is null)
                throw new NotFoundException("Nie znaleziono zbiórek");

            var collectionsDtoS = _mapper.Map<List<CollectionDto>>(collections);

            return collectionsDtoS;
        }
        #endregion
        #region Pobierz konkretną zbiórkę
        public CollectionDto GetCollection(int id)
        {
            var collection = _dbContext
                .Collections
                .Include(_r => _r.CollectionStatus)
                .FirstOrDefault(r => r.Id == id);

            if (collection is null)
                throw new NotFoundException("Nie znaleziono zbiórki");

            var collectionDto = _mapper.Map<CollectionDto>(collection);

            return collectionDto;
        }
        #endregion
        #region Zmiana statusu zbiórki
        public void UpdateCollectionStatus(int id, UpdateCollectionStatusDto dto)
        {
            var collection = _dbContext
                .Collections
                .Include(r => r.CreatedByOrganiser)
                .ThenInclude(r => r.Role)
                .FirstOrDefault(r => r.Id == id);

            if (collection is null)
                throw new NotFoundException("Nie znaleziono zbiórki");


            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, collection,
               new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("Nie masz uprawnień do edycji");
            }

            if (dto.CollectionStatusId != null)
            {
                collection.CollectionStatusId = dto.CollectionStatusId;
            }
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
                throw new ForbidException("Brak uprawnień do usunięcie zbiórki");
            }

            _dbContext.Collections.Remove(collection);
            _dbContext.SaveChanges();
        }
        #endregion
        #region Lista wszystkich zbiórek z adresem
        public IEnumerable<CollectionPointDto> GetAll()
        {
            var collections = _dbContext
                .Collections
                .Include(r => r.CollectionPoints)
                .ThenInclude(n => n.Address)
                .ToList();

            if (collections is null) throw new NotFoundException("Collection not found");


            var collectionDtos = _mapper.Map<List<CollectionPointDto>>(collections);

            return collectionDtos;
        }
        #endregion
        #region Lista produktów potrzebnych w zbiórce

        public List<CollectionProductsListDto> GetAllProducts(int collectionId)
        {
            var collection = _dbContext
                .Collections
                .Include(a => a.CollectionProducts.Where(cp => cp.Quantily > 0))

                .ThenInclude(a => a.Product)
                .FirstOrDefault(r => r.Id == collectionId);

            var collectionProductsDtos = _mapper.Map<List<CollectionProductsListDto>>(collection.CollectionProducts);

            return collectionProductsDtos;
        }
        #endregion
        #region Dodanie potrzebnych przedmiotów do zbiórki
        public int CreateCollectionNeededProducts(int collectionId, CreateCollectionProductDto dto)
        {

            var productInUse = _dbContext
              .CollectionProducts
              .Where(r => r.ProductId == dto.ProductId)
              .Where(r => r.CollectionId == collectionId)
              .Any();
            if (productInUse)
                throw new BadRequestException("Produkt już istnieje");


            var collectionProduct = _mapper.Map<CollectionProduct>(dto);
            collectionProduct.CollectionId = collectionId;    

            _dbContext.CollectionProducts.Add(collectionProduct);
            _dbContext.SaveChanges();

            return collectionProduct.Id;
        }
        #endregion


        public IEnumerable <CollectionDto> GetCollectionPerOrganiser(int id)
        {
            var collection = _dbContext
                .Collections
                .Include(x =>x.CollectionStatus)
                .Where(r => r.CreatedByOrganiserId == id)
                .OrderByDescending(x => x.CollectionStatus)
                .ThenByDescending(x => x.CreateDate)
                .ToList();

            if (collection is null) throw new NotFoundException("Nie znaleziono zbiórek");

            var collectionDtos = _mapper.Map<List<CollectionDto>>(collection);

            return collectionDtos;
        }

        public void EditCollection(int collectionId, EditCollectionDto dto)
        {
            var collection = _dbContext
                .Collections
                .FirstOrDefault(x => x.Id == collectionId);

            if (collection == null)
                throw new NotFoundException("nie znaleziono zbiórki");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, collection,
             new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("Nie masz uprawnień do edycji zbiórki");
            }

            if (dto.RegistrationNumber != null)
            {
                collection.RegistrationNumber = dto.RegistrationNumber;
            }
            if (dto.RegistrationNumber != null)
            {
                collection.Title = dto.Title;
            }
            if (dto.Description != null)
            {
                collection.Description = dto.Description;
            }
            if (dto.CollectionStatusId != null)
            {
                collection.CollectionStatusId = dto.CollectionStatusId;
            }
            _dbContext.SaveChanges();
        }

        public void DeleteCollectionproduct(int id, int collectionId)
        {

            var collectionProduct = _dbContext
                .CollectionProducts
                .Where(a => a.CollectionId == collectionId)
                .FirstOrDefault(r => r.Id == id);

            if (collectionProduct is null)
                throw new NotFoundException("Nie znaleziono produktu");

            _dbContext.CollectionProducts.Remove(collectionProduct);
            _dbContext.SaveChanges();

        }
        public void EditCollectionProduct(int collectionProductId, int collectionId, CreateCollectionProductDto dto)
        {
            var collectionProduct = _dbContext
                .CollectionProducts
                .Where(r => r.CollectionId == collectionId)
                .FirstOrDefault(x => x.Id == collectionProductId);

            if (collectionProduct == null)
                throw new NotFoundException("nie znaleziono przedmiotu");

            if (dto.Quantily != null)
            {
                collectionProduct.Quantily = dto.Quantily;
            }
            if (dto.ShortDescription != null)
            {
                collectionProduct.ShortDescription = dto.ShortDescription;
            }
    
            _dbContext.SaveChanges();
        }
        public CollectionProductsListDto GetCollectionProductById(int collectionId, int collectionProductId)
        {
            var collectionProduct = _dbContext.CollectionProducts
                 .Include(x => x.Product)
                .Where(r => r.CollectionId == collectionId)
               
                .FirstOrDefault(d => d.Id == collectionProductId);
            if (collectionProduct == null)
                throw new NotFoundException("nie znaleziono punktu");

            var collectionProductDto = _mapper.Map<CollectionProductsListDto>(collectionProduct);
            return collectionProductDto;
        }
    }
}
