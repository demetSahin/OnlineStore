using AutoMapper;
using OnlineStore.Business.Dtos;
using OnlineStore.DAL.Entities;
using OnlineStore.WebUI.Models;

namespace OnlineStore.WebUI.Mappings
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserAddDto, RegisterViewModel>().ReverseMap();
            CreateMap<UserLoginDto, LoginViewModel>().ReverseMap();
            CreateMap<CategoryAddDto, CategoryViewModel>().ReverseMap();
            CreateMap<ShoppingBasketDto, ShoppingBasketViewModel>().ReverseMap();
            CreateMap<ProductDto, ProductViewModel>().ReverseMap();
            CreateMap<UserDto, UserViewModel>().ReverseMap();
            CreateMap<ProductLikeDto, ProductLikeViewModel>().ReverseMap();
            CreateMap<ShippingDetailsDto, ShippingDetailsViewModel>().ReverseMap();
            CreateMap<OrderAddDto,OrderViewModel>().ReverseMap();
            CreateMap<OrderDto, OrderViewModel>().ReverseMap();
            CreateMap<OrderDetailDto, OrderDetailViewModel>().ReverseMap();
            CreateMap<CommentDto, CommentViewModel>().ReverseMap();

        }

    }
}
