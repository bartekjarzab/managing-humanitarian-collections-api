using AutoMapper;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Models.Collection;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace managing_humanitarian_collections_api.Services
{
    public interface ICollectionPointService
    {
        int Create(CreateCollectionPointDto dto);
        int CreatePointForCollection(int collectionId, CreateCollectionPointDto dto);
        CollectionPointDto GetById(int collectionId, int collectionPointId);
        List<CollectionPointDto> GetAll(int collectionId);

        Collection GetCollectionPointById(int collectionId);

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

        public int Create(CreateCollectionPointDto dto)
        {
            var collectionPoint = _mapper.Map<CollectionPoint>(dto);
            _dbContext.CollectionPoints.Add(collectionPoint);
            _dbContext.SaveChanges();

            return collectionPoint.Id;
        }

        public int CreatePointForCollection(int collectionId, CreateCollectionPointDto dto)
        {
           // var collection = GetCollectionById(collectionId);
            var collectionPointEntity = _mapper.Map<CollectionPoint>(dto);
      
            collectionPointEntity.CollectionId = collectionId;

            _dbContext.CollectionPoints.Add(collectionPointEntity);
            _dbContext.SaveChanges();

            return collectionPointEntity.Id;
        }
       
        //Collection + CollectionPoints
        public Collection GetCollectionPointById(int collectionId)
        {
            var collection = _dbContext
                .Collections
                .Include(r => r.CollectionPoints)
                .ThenInclude(r =>r.Address)
                .FirstOrDefault(r => r.Id == collectionId);
            
            return collection;
        }
        public CollectionPointDto GetById(int collectionId, int collectionPointId)
        {
            var collection = _dbContext.CollectionPoints.FirstOrDefault(r => r.Id == collectionId);

            //var collection = GetCollectionById(collectionId);

            var collectionPoint = _dbContext.CollectionPoints
                .Where(r => r.Id == collectionId)
                .Include(r => r.Address)
                .FirstOrDefault(d => d.Id == collectionPointId);
            if (collectionPoint == null || collectionPoint.CollectionId != collectionId)
               {
                //wyrzucic wyjątek
                  }
            var collectionPointDto = _mapper.Map<CollectionPointDto>(collectionPoint);
            return collectionPointDto;
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
    }  
}
