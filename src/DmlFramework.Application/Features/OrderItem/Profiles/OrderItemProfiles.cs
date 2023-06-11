using AutoMapper;
using DmlFramework.Application.Features.OrderItem.Models;
using DmlFramework.Domain.Entities;
using DmlFramework.Application.Features.Order.Commands;
using DmlFramework.Application.Features.Order.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmlFramework.Application.Features.OrderItem.Profiles
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItemEntity, OrderItemResponse>().ReverseMap();
        }
    }
}
