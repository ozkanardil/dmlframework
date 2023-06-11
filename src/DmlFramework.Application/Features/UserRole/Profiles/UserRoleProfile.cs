using AutoMapper;
using DmlFramework.Application.Features.UserRole.Command;
using DmlFramework.Application.Features.UserRole.Models;
using DmlFramework.Domain.Entities;
using DmlFramework.Application.Features.Product.Commands;
using DmlFramework.Application.Features.Product.Models;
using DmlFramework.Application.Features.User.Commands;
using DmlFramework.Application.Features.User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
