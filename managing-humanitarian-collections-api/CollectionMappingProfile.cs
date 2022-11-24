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
            CreateMap<CreateProductCategoryDto, ProductCategory>();
            CreateMap<CreateCollectionPointDto, CollectionPoint>()
                .ForMember(p => p.Address, c => c.MapFrom(dto => new Address()
                { Voivodeship = dto.Voivodeship, 
                    Street = dto.Street, 
                    City = dto.City,
                    Postcode = dto.Postcode,
                    HouseNumber = dto.HouseNumber, 
                    Apartment = dto.Apartment }));

            CreateMap<CreateCollectionDto, Collection>();
            CreateMap<Product, ProductWithoutPropertiesDto>();
            CreateMap<ProductCategory, CategoriesDto>()
                .ForMember(m =>m.Products, c => c.MapFrom(s => s.Products));
                
            //wszystkie zbiórki z punktami + adresy 
            CreateMap<CollectionPoint, CollectionPointDto>()
                .ForMember(m => m.Voivodeship, c => c.MapFrom(s => s.Address.Voivodeship))
                  .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
                  .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                  .ForMember(m => m.Postcode, c => c.MapFrom(s => s.Address.Postcode))
                  .ForMember(m => m.HouseNumber, c => c.MapFrom(s => s.Address.HouseNumber))
                  .ForMember(m => m.Apartment, c => c.MapFrom(s => s.Address.Apartment));
            
            CreateMap<CollectionProduct, CollectionProductDto>()
                .ForMember(m => m.ProductName, c => c.MapFrom(s => s.Product.Name));


            CreateMap<Collection, CollectionDto>();
            CreateMap<CollectionDto, Collection>();
            CreateMap<Collection, CollectionWithAddressDto>()
                .ForMember(o => o.CollectionPoints, c => c.MapFrom(s => s.CollectionPoints));

            CreateMap<Collection, CollectionProductsNeededDto>()
                .ForMember(o => o.Products, c => c.MapFrom(s => s.CollectionProducts))
                .ForMember(o => o.CollectionId, c => c.MapFrom(s => s.Id));
        }     
    }
}
