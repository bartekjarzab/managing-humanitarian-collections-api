using AutoMapper;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Models;
using managing_humanitarian_collections_api.Models.Collection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace managing_humanitarian_collections_api.Services
{

    public interface IProductService
    {
        CategoryProductsDto GetProductsByCategory(int id);

        List<CategeriesDto> GetAllCategories();

        ProductWithPropertiesDto GetProductWithProperties(int id);

        int AddProductsToCategory(int categoryId, AddProductToCategoryDto dto);
        public List<ProductDto> GetAllProducts(string search);


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


        public List<CategeriesDto> GetAllCategories()
        {
            var categories = _dbContext
                .ProductCategories
                .ToList();

            var collectionProductsDtos = _mapper.Map<List<CategeriesDto>>(categories);

            return collectionProductsDtos;
        }

        public CategoryProductsDto GetProductsByCategory(int id)
        {
            var products = _dbContext
                .ProductCategories
                .Include(c => c.Products)
                .FirstOrDefault(r => r.Id == id);

            var productsByCategoryDtos = _mapper.Map<CategoryProductsDto>(products);

            return productsByCategoryDtos;
        }

        public ProductWithPropertiesDto GetProductWithProperties(int id)
        {
            var product = _dbContext
                .Products
                .Include(c => c.Properties)
                .FirstOrDefault(p => p.Id == id);

            var productDtos = _mapper.Map<ProductWithPropertiesDto>(product);

            return productDtos;

        }

        public int AddProductsToCategory(int categoryId, AddProductToCategoryDto dto)
        {
            var product = _mapper.Map<Product>(dto);

             product.ProductCategoryId = categoryId;

            _dbContext.Products.Add(product);

            _dbContext.SaveChanges();

            return product.Id;

        }
        public List<ProductDto> GetAllProducts(string search)
        {
            var products = _dbContext
                .Products
                .Include(r => r.Category)
                .Where(r => search == null || (r.Name.ToLower().Contains(search.ToLower())))
                .ToList();

            var productDtos = _mapper.Map<List<ProductDto>>(products);

            return productDtos;
        }

    }
}
