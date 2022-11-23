using AutoMapper;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Models;
using managing_humanitarian_collections_api.Models.QueryValidators;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace managing_humanitarian_collections_api.Services
{
    public interface ICollectionService
    {
        int Create(CreateCollectionDto dto);
        CollectionDto GetById(int id);
        IEnumerable<CollectionDto> GetAll();
       
        List<int> GetCollectionPointIds(int collectionId);
    }
    public class CollectionService : ICollectionService
    {
        private readonly ManagingCollectionsDbContext _dbContext;
        private readonly IMapper _mapper;

        public CollectionService(ManagingCollectionsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int Create(CreateCollectionDto dto)
        {
            var collection = _mapper.Map<Collection>(dto);
            _dbContext.Collections.Add(collection);
            _dbContext.SaveChanges();

            return collection.Id;
        }
        public IEnumerable<CollectionDto> GetAll()
        {
            var collections = _dbContext
                .Collections
                .Include(r => r.CollectionPoints)
                .ToList();

            var collectionDtos = _mapper.Map<List<CollectionDto>>(collections);

            return collectionDtos;
        }

        public CollectionDto GetById(int id)
        {
            var collection = _dbContext
                .Collections
                .Include(n => n.CollectionPoints)
                .FirstOrDefault(r => r.Id == id);

            // if(collection is null) exception

            var result = _mapper.Map<CollectionDto>(collection);
            return result;
        }

        //public CollectionWithAdressDto GetCollectionWithAddress(int id)
        //{
        //    var collection = _dbContext
        //        .Collections
        //        .FirstOrDefault(r => r.Id == id);

        //    var mappedCollection =_mapper.Map<CollectionDto>(collection); 
        // //   var collectionPointId = Get
        //}

        //lista Id przypisana do kolekcji
        public List<int> GetCollectionPointIds(int collectionId)
        {
            var result = _dbContext
                .CollectionPoints
                .Where(r => r.CollectionId == collectionId)
                .Select(r => r.Id)
                .ToList();

            return result;
        }
    }
}
