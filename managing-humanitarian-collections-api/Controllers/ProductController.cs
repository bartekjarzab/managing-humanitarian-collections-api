using AutoMapper;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Models;
using managing_humanitarian_collections_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace managing_humanitarian_collections_api.Controllers
{


    [Route("api/collection")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
    
            [HttpGet("{id}/products")]
            public ActionResult<CollectionProductsNeededDto> Get([FromRoute] int id)
            {
                var collection = _productService.GetById(id);
                return Ok(collection);
            }
        [HttpGet("products/{name}")]
        public ActionResult<CategoriesDto> GetProductsByCategory([FromRoute] string name)
        {
            var category = _productService.GetProductsByCategory(name);
            return Ok(category);
        }


    }
}

