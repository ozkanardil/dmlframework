using AutoMapper;
using DmlFramework.Application.Features.Order.Commands;
using DmlFramework.Application.Features.Order.Models;
using DmlFramework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmlFramework.Application.Features.Order.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderCreateDto, CreateOrderCommand>().ReverseMap();
            CreateMap<OrderEntity, OrderResponse>().ReverseMap();
            CreateMap<CreateOrderCommand, OrderEntity>().ReverseMap();
        }
    }
}
