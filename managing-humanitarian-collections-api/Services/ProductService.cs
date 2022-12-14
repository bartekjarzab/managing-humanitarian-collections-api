using AutoMapper;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace managing_humanitarian_collections_api.Services
{

    public interface IProductService
    {
        CollectionProductsNeededDto GetById(int id);
        List<CategoriesDto> GetProductsByCategory(string name);
    }
    public class ProductService : IProductService
    {
        private readonly ManagingCollectionsDbContext _dbContext;
        private readonly IMapper _mapper;
        public ProductService(ManagingCollectionsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public CollectionProductsNeededDto GetById(int id)
        {
            var collection = _dbContext.Collections
                .Include(n => n.CollectionProducts)
                .ThenInclude(n => n.Product)
                .FirstOrDefault(r => r.Id == id);

            // if(collection is null) exception

            var result = _mapper.Map<CollectionProductsNeededDto>(collection);
            return result;
        }

        public List<CategoriesDto> GetProductsByCategory(string name)
        {
            var categories = _dbContext.ProductCategories
                .Include(c => c.Products)
                .Where(o => o.Name == name)
                .ToList();

            var productsByCategoryDtos = _mapper.Map<List<CategoriesDto>>(categories);

            return productsByCategoryDtos;
        }
    }
}
