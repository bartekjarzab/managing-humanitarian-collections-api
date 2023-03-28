using Microsoft.AspNetCore.Mvc;
using managing_humanitarian_collections_api.Services;
using managing_humanitarian_collections_api.Models;
using managing_humanitarian_collections_api.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace managing_humanitarian_collections_api.Controllers
{

    [Route("/api")]
    //[ApiController]
    //[Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet("categories/{categoryId}/products")]
        public ActionResult <CategoryProductsDto> Get([FromRoute] int categoryId)
        {
            var result = _productService.GetProductsByCategory(categoryId);
            return Ok(result);
        }

        [HttpGet("categories")]
        public ActionResult<IEnumerable<CategeriesDto>> GetAllCategories()
        {
            var categoriesDtos = _productService.GetAllCategories();

            return Ok(categoriesDtos);
        }

        [HttpGet("products/{productId}")]
        public ActionResult<ProductWithPropertiesDto> GetProduct([FromRoute] int productId)
        {
            var result = _productService.GetProductWithProperties(productId);
            return Ok(result);
        }

        [HttpPost("categories/{categoryId}/products")]
        public ActionResult AddProduct([FromRoute] int categoryId, [FromBody] AddProductToCategoryDto dto)
        {
            var newProductId = _productService.AddProductsToCategory(categoryId, dto);

            return Created($"api/categories/{categoryId}/products/{newProductId}", null);

        }
    }
}
