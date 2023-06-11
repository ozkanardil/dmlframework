using AutoMapper;
using DmlFramework.Application.Features.Auth.Models;
using DmlFramework.Application.Features.UserRole.Models;
using DmlFramework.Domain.Entities;
using DmlFramework.Infrastructure.Security.JwtToken;

namespace DmlFramework.Application.Features.Auth.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<UserRoleVEntity, UserRoleResponse>().ReverseMap();

            CreateMap<AccessToken, TokenResult>().ReverseMap();

            CreateMap<UserRoleEntity, RoleEntity>().ReverseMap();

            CreateMap<UserRoleEntity, UserRoleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Role.Role))
                .ReverseMap();
        }
    }
}
