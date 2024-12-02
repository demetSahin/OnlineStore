using AutoMapper;
using OnlineStore.Business.Dtos;
using OnlineStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Business.Mappings
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<UserEntity, UserAddDto>().ReverseMap();
            CreateMap<UserEntity, UserInfoDto>().ReverseMap();
            CreateMap<UserEntity, UserLoginDto>().ReverseMap();
            CreateMap<UserEntity, UserDto>().ReverseMap();
            CreateMap<CategoryEntity, CategoryAddDto>().ReverseMap();
            CreateMap<ShoppingBasket, ShoppingBasketDto>().ReverseMap();
            CreateMap<ProductEntity, ProductDto>().ReverseMap();
            CreateMap<OrderEntity, OrderAddDto>().ReverseMap();
            CreateMap<OrderEntity, OrderDto>().ReverseMap();
            CreateMap<OrderDetailEntity, OrderDetailDto>().ReverseMap();
            CreateMap<CommentEntity, CommentDto>().ReverseMap();
            CreateMap<ProductLikeEntity, ProductLikeDto>().ReverseMap()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Product, opt => opt.Ignore()) // Product nesnesini maplemiyoruz
            .ForMember(dest => dest.User, opt => opt.Ignore()); // User nesnesini maplemiyoruz;

        }
    }
}
