using AutoMapper;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Models;
using managing_humanitarian_collections_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace managing_humanitarian_collections_api.Controllers
{


    [Route("api/categories")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }
        //[Authorize(Roles = "Organizer")]
        //public ActionResult<IEnumerable<ProductCategoryDto>> GetAllCategories()
        //{
        //    var categories = _dbContext
        //        .ProductCategories
        //        .ToList();
        //    //dla kazdej kategorii z bazy danych tworzy nowy obiekt(dto) 

        //    var categoriesDtos = _mapper.Map<List<ProductCategoryDto>>(categories);


        //    return Ok(categoriesDtos);
        //}
        [HttpPost]
        public ActionResult CreateProductCategory([FromBody] CreateProductCategoryDto dto)
        {
          //  var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var id = _productCategoryService.Create(dto);

            return Created($"/api/categories/{id}", null);
        }


       
    }
}
