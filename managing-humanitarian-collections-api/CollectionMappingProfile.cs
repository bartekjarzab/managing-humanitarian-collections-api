using AutoMapper;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Models;
using managing_humanitarian_collections_api.Models.Admin;
using managing_humanitarian_collections_api.Models.Collection;
using System;

namespace managing_humanitarian_collections_api
{
    public class CollectionMappingProfile : AutoMapper.Profile
    {
        public CollectionMappingProfile()
        {
            CreateMap<ProductCategory, ProductCategoryDto>();
            CreateMap<User, UsersDto>()
                .ForMember(u => u.RoleName, c => c.MapFrom(s => s.Role.Name));
            CreateMap<ProductCategoryDto, ProductCategory>();
            CreateMap<Product, CreateProductDto>();
            CreateMap<ProductProperties, ProductPropertiesDto>();
            CreateMap<CreateProductCategoryDto, ProductCategory>();
            CreateMap<CollectionProduct, CreateCollectionProductDto>();
            CreateMap<CreateOrderDto, Order>();
            CreateMap<Order, CreateOrderDto>();
            CreateMap<AddProductToOrderDto, OrderProduct>();
            CreateMap<CreateCollectionProductDto, CollectionProduct>();
            CreateMap<CollectionProduct, CollectionWithProductsDto>();
            CreateMap<Order, OrderDto>()
                .ForMember(o => o.Orders, c => c.MapFrom(s => s.OrderProducts));
            CreateMap<ProductCategory, CategoryProductsDto>()
                .ForMember(o => o.ProductList, c => c.MapFrom(s =>s.Products));
            CreateMap<CollectionProduct, CollectionProductsListDto>()
                .ForMember(o => o.ProductName, c => c.MapFrom(s => s.Product.Name));
            CreateMap<CreateCommentDto, Comment>();

            CreateMap<Comment, CommentDto>()
                .ForMember(o => o.FirstName, c => c.MapFrom(s => s.CreatedBy.Profile.FirstName))
                .ForMember(o => o.LastName, c => c.MapFrom(s => s.CreatedBy.Profile.LastName));
            CreateMap<User, UserProfileDto>()
                .ForMember(u => u.Name, c => c.MapFrom(r => r.Profile.Name))
                .ForMember(u => u.FirstName, c => c.MapFrom(r => r.Profile.FirstName))
                .ForMember(u => u.LastName, c => c.MapFrom(r => r.Profile.LastName))
                .ForMember(u => u.Nip, c => c.MapFrom(r => r.Profile.Nip))
                .ForMember(u => u.Regon, c => c.MapFrom(r => r.Profile.Regon))
                .ForMember(u => u.ContactNumber, c => c.MapFrom(r => r.Profile.ContactNumber))
                .ForMember(u => u.Avatar, c => c.MapFrom(r => r.Profile.Avatar));
            CreateMap<Product, ProductDto>()
                .ForMember(o => o.Category, c => c.MapFrom(s => s.Category.Name));
            CreateMap<Product, AddProductToCategoryDto>();
            CreateMap<ProductCategory, CategeriesDto>();
            CreateMap<CreateCollectionPointDto, CollectionPoint>()
                .ForMember(p => p.Address, c => c.MapFrom(dto => new Address()
                { VoivodeshipId = dto.VoivodeshipId, 
                    Street = dto.Street, 
                    City = dto.City,
                    Postcode = dto.Postcode,
                    HouseNumber = dto.HouseNumber, 
                    Apartment = dto.Apartment }));
            CreateMap<AddProductToCategoryDto, Product>();

            CreateMap<Order, OrdersPerDonator>();
          //  CreateMap<AddProductToCategoryDto, ProductCategory>();
                //.ForMember(p => p.Products, c => c.MapFrom(dto => new Product()
                //{
                //    Name = dto.Name
                //}));
            CreateMap<CreateCollectionDto, Collection>();
            CreateMap<CollectionPoint, CollectionPointDto>()
                .ForMember(m => m.Voivodeship, c => c.MapFrom(s => s.Address.Voivodeship.Name))
                  .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
                  .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                  .ForMember(m => m.Postcode, c => c.MapFrom(s => s.Address.Postcode))
                  .ForMember(m => m.HouseNumber, c => c.MapFrom(s => s.Address.HouseNumber))
                  .ForMember(m => m.Apartment, c => c.MapFrom(s => s.Address.Apartment));
            CreateMap<Product, ProductWithPropertiesDto>()
                .ForMember(m => m.Width, c => c.MapFrom(s => s.Properties.Width))
                .ForMember(m => m.Size, c => c.MapFrom(s => s.Properties.Size))
                .ForMember(m => m.Weight, c => c.MapFrom(s => s.Properties.Weight))
                .ForMember(m => m.Height, c => c.MapFrom(s => s.Properties.Height));
            CreateMap<Collection, CollectionWithProductsDto>()
                .ForMember(m => m.CollectionProducts, c => c.MapFrom(s => s.CollectionProducts));
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
