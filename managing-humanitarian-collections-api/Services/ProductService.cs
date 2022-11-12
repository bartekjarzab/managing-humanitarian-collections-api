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
        
    }
}
