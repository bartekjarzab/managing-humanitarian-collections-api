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
                .ForMember(p => p.Address,
                c => c.MapFrom(dto => new Address()
                { Voivodeship = dto.Voivodeship, 
                    Street = dto.Street, 
                    City = dto.City,
                    Postcode = dto.Postcode,
                    HouseNumber = dto.HouseNumber, 
                    Apartment = dto.Apartment }));

            CreateMap<CreateCollectionDto, Collection>();
            CreateMap<CollectionPoint, CollectionPointDto>()
                .ForMember(m => m.Voivodeship, c => c.MapFrom(s => s.Address.Voivodeship))
                  .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
                  .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                  .ForMember(m => m.Postcode, c => c.MapFrom(s => s.Address.Postcode))
                  .ForMember(m => m.HouseNumber, c => c.MapFrom(s => s.Address.HouseNumber))
                  .ForMember(m => m.Apartment, c => c.MapFrom(s => s.Address.Apartment));
            CreateMap<Collection, CollectionDto>();
            CreateMap<CollectionDto, Collection>();

                //.ForMember(p => p.Address,
                //c => c.MapFrom(dto => new Address()
                //{ Voivodeship = dto.Voivodeship, Street = dto.Street, City = dto.City, Postcode = dto.Postcode, HouseNumber = dto.HouseNumber, Apartment = dto.Apartment }));

            //          public string Voivodeship { get; set; }
            //public string Street { get; set; }
            //public string City { get; set; }
            //public string Postcode { get; set; }
            //public string HouseNumber { get; set; }
            //public string Apartment { get; set; }
        }
     
    }
}
