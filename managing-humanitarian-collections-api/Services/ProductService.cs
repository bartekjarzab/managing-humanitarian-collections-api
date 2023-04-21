using AutoMapper;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Exceptions;
using managing_humanitarian_collections_api.Models;
using managing_humanitarian_collections_api.Models.Collection;
using managing_humanitarian_collections_api.Models.Product;
using managing_humanitarian_collections_api.Models.Products;
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

        void DeleteProduct(int id);
        ProductPropertiesDto GetProductWithProperties(int id);

        int AddProductsToCategory(int categoryId, AddProductToCategoryDto dto);
        public List<ProductDto> GetAllProducts(string search);
        List<ProductDto> GetProductsByCategory(int id, string search);


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

            if (categories == null) throw new NotFoundException("Nie znaleziono zbiórek");
            var collectionProductsDtos = _mapper.Map<List<CategeriesDto>>(categories);

            return collectionProductsDtos;
        }

        public CategoryProductsDto GetProductsByCategory(int id)
        {
            var products = _dbContext
                .ProductCategories
                .Include(c => c.Products)
                .FirstOrDefault(r => r.Id == id);
            if (products == null) throw new NotFoundException("Brak przedmiotów dla tej kategorii");
            var productsByCategoryDtos = _mapper.Map<CategoryProductsDto>(products);

            return productsByCategoryDtos;
        }

        public ProductPropertiesDto GetProductWithProperties(int id)
        {
            var product = _dbContext
                .Products
                .FirstOrDefault(p => p.Id == id);

            var productDtos = _mapper.Map<ProductPropertiesDto>(product);
            return productDtos;
        }
        public void DeleteProduct(int id)
        {
            var product = _dbContext
                .Products
                .FirstOrDefault(c => c.Id == id);

            if (product is null)
                throw new NotFoundException("Nie znaleziono przedmiotu");

            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();

        }
        public int AddProductsToCategory(int categoryId, AddProductToCategoryDto dto)
        {
            var product = _mapper.Map<Product>(dto);
             product.ProductCategoryId = categoryId;

            var products = _dbContext
                .Products
                .Where(r => r.ProductCategoryId == categoryId)
                .ToList();

            foreach (var productItem in products)
                if (productItem.Name.ToLower().Contains(dto.Name.ToLower()))
                    throw new BadRequestException("Przedmiot już istnieje");
            

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
            if (products == null) throw new NotFoundException("Nie znaleziono przedmiotów");

            var productDtos = _mapper.Map<List<ProductDto>>(products);

            return productDtos;
        }

        public List<ProductDto> GetProductsByCategory(int id, string search)
        {
            var products = _dbContext
                .Products
                .Include(r => r.Category)
                .Where(r => r.ProductCategoryId == id)
                .Where(r => search == null || (r.Name.ToLower().Contains(search.ToLower())))
                .ToList();
            if (products == null) throw new NotFoundException("Nie znaleziono przedmiotów");

            var productDtos = _mapper.Map<List<ProductDto>>(products);

            return productDtos;
        }

    }
}
