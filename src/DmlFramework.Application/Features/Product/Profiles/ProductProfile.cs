using AutoMapper;
using DmlFramework.Application.Features.Product.Commands;
using DmlFramework.Application.Features.Product.Models;
using DmlFramework.Domain.Entities;
using DmlFramework.Application.Features.Category.Commands;

namespace DmlFramework.Application.Features.Product.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductEntity, ProductResponse>().ReverseMap();
            CreateMap<CreateProductCommand, ProductEntity>().ReverseMap();
            CreateMap<UpdateProductCommand, ProductEntity>().ReverseMap();
        }
    }
}
