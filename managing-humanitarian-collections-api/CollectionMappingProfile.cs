using AutoMapper;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Models;
using System;

namespace managing_humanitarian_collections_api
{
    public class CollectionMappingProfile : AutoMapper.Profile
    {
        public CollectionMappingProfile()
        {
            CreateMap<ProductCategory, ProductCategoryDto>();
            CreateMap<ProductCategoryDto, ProductCategory>();
            CreateMap<Product, ProductDto>();           
            CreateMap<ProductProperties, ProductPropertiesDto>();
      

    }

      
    }
}
