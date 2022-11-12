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


    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
    }
}

