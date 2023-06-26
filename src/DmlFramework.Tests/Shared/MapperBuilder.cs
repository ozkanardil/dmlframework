using AutoMapper;
using DmlFramework.Application.Features.Auth.Profiles;
using DmlFramework.Application.Features.Role.Profiles;
using DmlFramework.Application.Features.User.Profiles;
using DmlFramework.Application.Features.UserRole.Profiles;

namespace DmlFramework.Tests.Shared
{
    public static class MapperBuilder
    {
        public static IMapper UserMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserProfile>();
            });
            return mapperConfig.CreateMapper();
        }

        public static IMapper AuthMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AuthProfile>();
            });
            return mapperConfig.CreateMapper();
        }

        public static IMapper RoleMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RoleProfile>();
            });
            return mapperConfig.CreateMapper();
        }

        public static IMapper UserRoleMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserRoleProfile>();
            });
            return mapperConfig.CreateMapper();
        }
        
    }
}
