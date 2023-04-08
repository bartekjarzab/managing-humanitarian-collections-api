using AutoMapper;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Exceptions;
using managing_humanitarian_collections_api.Models.Collection;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace managing_humanitarian_collections_api.Services
{
    public interface ICollectionPointService
    {
        int CreatePointForCollection(int collectionId, CreateCollectionPointDto dto);
        CollectionPointDto GetById(int collectionId, int collectionPointId);
        List<CollectionPointDto> GetAll(int collectionId);
        void EditCollectionPoint(int collectionPointId, int collectionId, CreateCollectionPointDto dto);
        Collection GetCollectionPointById(int collectionId);
        void DeleteCollectionPoint(int id, int collectionId);

    }
    public class CollectionPointService : ICollectionPointService
    {
        private readonly ManagingCollectionsDbContext _dbContext;
        private readonly IMapper _mapper;

        public CollectionPointService(ManagingCollectionsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public int CreatePointForCollection(int collectionId, CreateCollectionPointDto dto)
        {
            var collectionPointEntity = _mapper.Map<CollectionPoint>(dto);
            collectionPointEntity.CollectionId = collectionId;

            _dbContext.CollectionPoints.Add(collectionPointEntity);
            _dbContext.SaveChanges();

            return collectionPointEntity.Id;
        }
        public Collection GetCollectionPointById(int collectionId)
        {
            var collectionPoint = _dbContext
                .Collections
                .Include(r => r.CollectionPoints)
                .ThenInclude(r =>r.Address)
                .FirstOrDefault(r => r.Id == collectionId);

            if (collectionPoint is null)
                throw new NotFoundException("nie znaleziono punktu");

            return collectionPoint;
        }
        public CollectionPointDto GetById(int collectionId, int collectionPointId)
        {
            var collectionPoint = _dbContext.CollectionPoints
                .Where(r => r.Id == collectionId)
                .Include(r => r.Address)
                .ThenInclude(a => a.Voivodeship)
                .FirstOrDefault(d => d.Id == collectionPointId);
            if (collectionPoint == null || collectionPoint.CollectionId != collectionId) 
                throw new NotFoundException("nie znaleziono punktu");
            
            var collectionPointDto = _mapper.Map<CollectionPointDto>(collectionPoint);
            return collectionPointDto;
        }
        public void EditCollectionPoint(int collectionPointId, int collectionId, CreateCollectionPointDto dto)
        {
            var collectionPoint = _dbContext
                .CollectionPoints
                .Include(r => r.Address)
                .Where(r => r.CollectionId == collectionId)
                .FirstOrDefault(x => x.Id == collectionPointId);

            if (collectionPoint == null)
                throw new NotFoundException("nie znaleziono punktu");

            if (dto.Name != null)
            {
                collectionPoint.Name = dto.Name;
            }
            if (dto.OpeningHour != null)
            {
                collectionPoint.OpeningHour = dto.OpeningHour;
            }
            if (dto.ClosingHour != null)
            {
                collectionPoint.ClosingHour = dto.ClosingHour;
            }
            if (dto.VoivodeshipId != null)
            {
                collectionPoint.Address.VoivodeshipId = dto.VoivodeshipId;
            }
            if (dto.City != null)
            {
                collectionPoint.Address.City = dto.City;
            }
            if (dto.Street != null)
            {
                collectionPoint.Address.Street = dto.Street;
            }
            if (dto.Postcode != null)
            {
                collectionPoint.Address.Postcode = dto.Postcode;
            }
            if (dto.HouseNumber != null)
            {
                collectionPoint.Address.HouseNumber = dto.HouseNumber;
            }
            if (dto.Apartment != null)
            {
                collectionPoint.Address.Apartment = dto.Apartment;
            }
            _dbContext.SaveChanges();
        }
            
        public List<CollectionPointDto> GetAll(int collectionId)
        {
            var collection = _dbContext
                .Collections
                .Include(a => a.CollectionPoints)
                .ThenInclude(a => a.Address)
                .ThenInclude(a => a.Voivodeship)
                .FirstOrDefault(r => r.Id == collectionId); 
    
            var collectionPointDtos = _mapper.Map<List<CollectionPointDto>>(collection.CollectionPoints);

            return collectionPointDtos;
        }

        public void DeleteCollectionPoint(int id, int collectionId)
        {

            var collectionPoint = _dbContext
                .CollectionPoints
                .Where(a => a.CollectionId == collectionId)
                .FirstOrDefault(r => r.Id == id);

            if (collectionPoint is null)
                throw new NotFoundException("Nie znaleziono punktu zbiórki");

            _dbContext.CollectionPoints.Remove(collectionPoint);
            _dbContext.SaveChanges();

        }
    }  
}
