using AutoMapper;
using DmlFramework.Application.Features.Category.Profiles;
using DmlFramework.Application.Features.Order.Profiles;
using DmlFramework.Application.Features.OrderItem.Profiles;
using DmlFramework.Application.Features.Product.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmlFramework.Tests.Shared
{
    public static class MapperBuilder
    {
        public static IMapper CategoryMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CategoryProfile>();
            });
            return mapperConfig.CreateMapper();
        }

        public static IMapper ProductMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductProfile>();
            });
            return mapperConfig.CreateMapper();
        }

        public static IMapper OrderMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<OrderProfile>();
            });
            return mapperConfig.CreateMapper();
        }

        public static IMapper OrderItemMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<OrderItemProfile>();
            });
            return mapperConfig.CreateMapper();
        }
    }
}
