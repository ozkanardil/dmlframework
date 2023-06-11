using AutoMapper;
using DmlFramework.Application.Features.User.Commands;
using DmlFramework.Application.Features.User.Models;
using DmlFramework.Domain.Entities;
using DmlFramework.Application.Features.Product.Commands;
using DmlFramework.Application.Features.Product.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmlFramework.Application.Features.User.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, UserResponse>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Surname))
                .ReverseMap();

            CreateMap<CreateUserCommand, UserEntity>().ReverseMap();
        }
    }
}
