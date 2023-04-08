using Microsoft.AspNetCore.Mvc;
using managing_humanitarian_collections_api.Services;
using managing_humanitarian_collections_api.Models;
using managing_humanitarian_collections_api.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using managing_humanitarian_collections_api.Models.Products;
using managing_humanitarian_collections_api.Models.Product;

namespace managing_humanitarian_collections_api.Controllers
{

    [Route("/api")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpDelete("products/{productId}")]
        public ActionResult DeleteProduct([FromRoute] int productId)
        {
            _productService.DeleteProduct(productId);
            return Ok("Produkt został usunięty");
        }

        [HttpGet("products")]
        public ActionResult <ProductDto> GetProducts([FromQuery] string search)
        {
            var result = _productService.GetAllProducts(search);
            return Ok(result);
        }

        [HttpGet("categories/{id}/products")]
        public ActionResult<ProductDto> GetProducts([FromRoute] int id, [FromQuery] string search)
        {
            var result = _productService.GetProductsByCategory(id, search);
            return Ok(result);
        }

        [HttpGet("categories")]
        public ActionResult<IEnumerable<CategeriesDto>> GetAllCategories()
        {
            var categoriesDtos = _productService.GetAllCategories();

            return Ok(categoriesDtos);
        }

        [HttpGet("products/{productId}")]
        public ActionResult<ProductPropertiesDto> GetProduct([FromRoute] int productId)
        {
            var result = _productService.GetProductWithProperties(productId);
            return Ok(result);
        }

        [HttpPost("categories/{categoryId}/products")]
        public ActionResult AddProduct([FromRoute] int categoryId, [FromBody] AddProductToCategoryDto dto)
        {
            var newProductId = _productService.AddProductsToCategory(categoryId, dto);

            return Ok(newProductId);

        }
    }
}
