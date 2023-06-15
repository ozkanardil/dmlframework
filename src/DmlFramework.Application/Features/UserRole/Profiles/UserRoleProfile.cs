using AutoMapper;
using DmlFramework.Application.Features.UserRole.Command;
using DmlFramework.Application.Features.UserRole.Models;
using DmlFramework.Domain.Entities;

namespace DmlFramework.Application.Features.UserRole.Profiles
{
    public class UserRoleProfile : Profile
    {
        public UserRoleProfile()
        {
            CreateMap<UserRoleVEntity, UserRoleResponse>().ReverseMap();
            CreateMap<CreateUserRoleCommand, UserRoleEntity>().ReverseMap();
        }
    }
}
