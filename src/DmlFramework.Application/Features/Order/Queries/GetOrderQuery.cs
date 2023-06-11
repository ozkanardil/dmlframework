using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DmlFramework.Infrastructure.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DmlFramework.Application.Features.Order.Models;
using DmlFramework.Application.Features.Order.Constants;
using DmlFramework.Infrastructure.Results;
using DmlFramework.Persistance.Context;

namespace DmlFramework.Application.Features.Order.Queries
{
    public class GetOrderQuery : IRequest<IRequestDataResult<IEnumerable<OrderResponse>>>
    {
        public int UserId { get; set; }
    }

    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, IRequestDataResult<IEnumerable<OrderResponse>>>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public GetOrderQueryHandler(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<IRequestDataResult<IEnumerable<OrderResponse>>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Orders.Where(ur => ur.UserId == request.UserId).ToListAsync();
            var response = _mapper.Map<IEnumerable<OrderResponse>>(result);

            if (!response.Any())
                return new ErrorRequestDataResult<IEnumerable<OrderResponse>>(response, Messages.OrderNoRecord);

            return new SuccessRequestDataResult<IEnumerable<OrderResponse>>(response, Messages.OrdersListed); ;
        }
    }
}
