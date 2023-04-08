using AutoMapper;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Models;
using managing_humanitarian_collections_api.Models.Admin;
using managing_humanitarian_collections_api.Models.Collection;
using managing_humanitarian_collections_api.Models.Comment;
using managing_humanitarian_collections_api.Models.Order;
using managing_humanitarian_collections_api.Models.Product;
using managing_humanitarian_collections_api.Models.Products;
using System;

namespace managing_humanitarian_collections_api.Common
{
    public class CollectionMappingProfile : AutoMapper.Profile
    {
        public CollectionMappingProfile()
        {

            #region Mapowanie Usera
            CreateMap<User, UsersDto>()
            .ForMember(u => u.RoleName, c => c.MapFrom(s => s.Role.Name));
            CreateMap<User, UserProfileDto>()
              .ForMember(u => u.Name, c => c.MapFrom(r => r.Profile.Name))
              .ForMember(u => u.FirstName, c => c.MapFrom(r => r.Profile.FirstName))
              .ForMember(u => u.LastName, c => c.MapFrom(r => r.Profile.LastName))
              .ForMember(u => u.Nip, c => c.MapFrom(r => r.Profile.Nip))
              .ForMember(u => u.Regon, c => c.MapFrom(r => r.Profile.Regon))
              .ForMember(u => u.ContactNumber, c => c.MapFrom(r => r.Profile.ContactNumber))
              .ForMember(u => u.Avatar, c => c.MapFrom(r => r.Profile.Avatar.Name));
            CreateMap<CollectionPoint, CollectionPointDto>()
               .ForMember(m => m.Voivodeship, c => c.MapFrom(s => s.Address.Voivodeship.Name))
               .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
               .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
               .ForMember(m => m.Postcode, c => c.MapFrom(s => s.Address.Postcode))
               .ForMember(m => m.HouseNumber, c => c.MapFrom(s => s.Address.HouseNumber))
               .ForMember(m => m.Apartment, c => c.MapFrom(s => s.Address.Apartment));
            #endregion
            #region Mapowanie zbiórek
            CreateMap<CreateCollectionDto, Collection>();
            CreateMap<CollectionProduct, CollectionProductsListDto>()
                .ForMember(o => o.ProductName, c => c.MapFrom(s => s.Product.Name))
                  .ForMember(o => o.Size, c => c.MapFrom(s => s.Product.Properties.Size))
             .ForMember(o => o.Weight, c => c.MapFrom(s => s.Product.Properties.Weight));
            CreateMap<Collection, CollectionDto>()
                .ForMember(m => m.Status, c => c.MapFrom(s => s.CollectionStatus.Status))
                .ForMember(m => m.Name, c => c.MapFrom(s => s.CreatedBy.Profile.Name));
            CreateMap<CreateCollectionPointDto, CollectionPoint>()
                .ForMember(p => p.Address, c => c.MapFrom(dto => new Address()
                {
                    VoivodeshipId = dto.VoivodeshipId,
                    Street = dto.Street,
                    City = dto.City,
                    Postcode = dto.Postcode,
                    HouseNumber = dto.HouseNumber,
                    Apartment = dto.Apartment
                }));


            CreateMap<CollectionDto, Collection>();
            CreateMap<Collection, CollectionPointDto>()
                .ForMember(p => p.Voivodeship, c => c.MapFrom(s => s.CollectionStatus.Status));
            CreateMap<Collection, CollectionWithProductsDto>()
              .ForMember(m => m.CollectionProducts, c => c.MapFrom(s => s.CollectionProducts));
            CreateMap<Collection, CollectionProductsNeededDto>()
                .ForMember(o => o.Products, c => c.MapFrom(s => s.CollectionProducts))
                .ForMember(o => o.CollectionId, c => c.MapFrom(s => s.Id));
            #endregion
            #region Mapowanie komentarzy
            CreateMap<CreateCommentDto, Comment>();
            CreateMap<Comment, CommentDto>()
               .ForMember(o => o.FirstName, c => c.MapFrom(s => s.CreatedBy.Profile.FirstName))
               .ForMember(o => o.LastName, c => c.MapFrom(s => s.CreatedBy.Profile.LastName));
               
            #endregion
            #region Mapowanie przedmiotów
            CreateMap<Product, CreateProductDto>()
                 .ForMember(o => o.Size, c => c.MapFrom(s => s.Properties.Size))
                 .ForMember(o => o.Weight, c => c.MapFrom(s => s.Properties.Weight));
            CreateMap<Product, ProductPropertiesDto>()
                 .ForMember(o => o.Size, c => c.MapFrom(s => s.Properties.Size))
                 .ForMember(o => o.Weight, c => c.MapFrom(s => s.Properties.Weight));
            CreateMap<CreateProductCategoryDto, ProductCategory>();
            CreateMap<CollectionProduct, CreateCollectionProductDto>();
            CreateMap<CreateCollectionProductDto, CollectionProduct>();
            CreateMap<CollectionProduct, CollectionWithProductsDto>();
            CreateMap<Product, ProductDto>()
             .ForMember(o => o.Category, c => c.MapFrom(s => s.Category.Name))
             .ForMember(o => o.Size, c => c.MapFrom(s => s.Properties.Size))
             .ForMember(o => o.Weight, c => c.MapFrom(s => s.Properties.Weight));
                CreateMap<Product, AddProductToCategoryDto>();
            CreateMap<ProductCategory, CategeriesDto>();

            CreateMap<AddProductToCategoryDto, Product>()
                .ForMember(x => x.Properties, c => c.MapFrom(dto => new ProductProperties()
                {
                    Size = dto.Size,
                    Weight = dto.Weight
                }));
            #endregion
            #region Mapowanie zamówień
            CreateMap<CreateOrderDto, Order>();
            CreateMap<AddProductToOrderDto, OrderProduct>();
            CreateMap<Order, OrderDto>()
               .ForMember(o => o.Status, c => c.MapFrom(s => s.OrderStatus.Status))
               .ForMember(o => o.FirstName, c => c.MapFrom(s => s.CreatedBy.Profile.FirstName))
               .ForMember(o => o.LastName, c => c.MapFrom(s => s.CreatedBy.Profile.LastName))
                .ForMember(o => o.UserId, c => c.MapFrom(s => s.CreatedBy.Profile.Id));
            CreateMap<ProductCategory, CategoryProductsDto>()
                .ForMember(o => o.ProductList, c => c.MapFrom(s => s.Products));
            CreateMap<Order, OrdersPerDonator>();
            CreateMap<OrderProduct, OrderProductListDto>()
                .ForMember(o => o.Name, c => c.MapFrom(s => s.Product.Name));
            #endregion
        }
    }
}
