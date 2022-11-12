using AutoMapper;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace managing_humanitarian_collections_api.Controllers
{


    [Route("api/categories")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly ManagingCollectionsDbContext _dbContext;
        private readonly IMapper _mapper;
        public ProductCategoryController(ManagingCollectionsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<ProductCategoryDto>> GetAllCategories()
        {
            var categories = _dbContext
                .ProductCategories
                .Include(r => r.Products)
                .ToList();
            //dla kazdej kategorii z bazy danych tworzy nowy obiekt(dto) 

            var categoriesDtos = _mapper.Map<List<ProductCategoryDto>>(categories);


            return Ok(categoriesDtos);
        }


       
    }
}
