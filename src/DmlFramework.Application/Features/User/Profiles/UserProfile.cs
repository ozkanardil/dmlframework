using AutoMapper;
using DmlFramework.Application.Features.User.Commands;
using DmlFramework.Application.Features.User.Models;
using DmlFramework.Domain.Entities;

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
