using AutoMapper;
using DmlFramework.Application.Features.Role.Models;
using DmlFramework.Domain.Entities;

namespace DmlFramework.Application.Features.Role.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleEntity, RoleResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Role))
                .ReverseMap();
        }
    }
}
