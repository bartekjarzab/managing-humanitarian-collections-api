using AutoMapper;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace managing_humanitarian_collections_api.Services
{

    public interface IProductCategoryService
    {
        int Create(CreateProductCategoryDto dto);
    }
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly ManagingCollectionsDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        public ProductCategoryService(ManagingCollectionsDbContext dbContext, IMapper mapper, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userContextService = userContextService;

        }

        public int Create(CreateProductCategoryDto dto)
        {
            var productcategory = _mapper.Map<ProductCategory>(dto);
           // productcategory.CreatedById = _userContextService.GetUserId;
            _dbContext.ProductCategories.Add(productcategory);
            _dbContext.SaveChanges();

            return productcategory.Id;
        }
            
    }
}
