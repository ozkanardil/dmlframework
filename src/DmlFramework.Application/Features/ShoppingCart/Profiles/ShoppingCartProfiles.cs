using AutoMapper;
using DmlFramework.Application.Features.ShoppingCart.Commands;
using DmlFramework.Application.Features.ShoppingCart.Models;
using DmlFramework.Domain.Entities;


namespace DmlFramework.Application.Features.ShoppingCart.Profiles
{
    public class ShoppingCartProfile : Profile
    {
        public ShoppingCartProfile()
        {
            CreateMap<ShoppingCartEntity, ShoppingCartResponse>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ReverseMap();

            CreateMap<CreateShoppingCartCommand, ShoppingCartEntity>().ReverseMap();

            CreateMap<DeleteShoppingCartCommand, ShoppingCartEntity>().ReverseMap();

            CreateMap<CreateShoppingCartCommand, ShoppingCartCreateDto>().ReverseMap();
        }
    }
}
