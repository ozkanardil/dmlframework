using AutoMapper;
using DmlFramework.Application.Features.Category.Commands;
using DmlFramework.Application.Features.Category.Models;
using DmlFramework.Domain.Entities;

namespace DmlFramework.Application.Features.Category.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryEntity, CategoryResponse>().ReverseMap();
            CreateMap<CreateCategoryCommand, CategoryEntity>().ReverseMap();
            CreateMap<UpdateCategoryCommand, CategoryEntity>().ReverseMap();
        }
    }
}
