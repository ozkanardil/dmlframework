﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DmlFramework.Infrastructure.Results;
using DmlFramework.Application.Features.ShoppingCart.Models;
using DmlFramework.Application.Features.ShoppingCart.Constants;
using DmlFramework.Infrastructure.Results;
using DmlFramework.Persistance.Context;

namespace DmlFramework.Application.Features.ShoppingCart.Queries
{
    public class GetShoppingCartQuery : IRequest<IRequestDataResult<IEnumerable<ShoppingCartResponse>>>
    {
        public int UserId { get; set; }
    }

    public class GetShoppingCartQueryHandler : IRequestHandler<GetShoppingCartQuery, IRequestDataResult<IEnumerable<ShoppingCartResponse>>>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public GetShoppingCartQueryHandler(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<IRequestDataResult<IEnumerable<ShoppingCartResponse>>> Handle(GetShoppingCartQuery request, CancellationToken cancellationToken)
        {
            int userId = request.UserId;
            var result = await _context.ShoppingCart.Where(sc => sc.UserId == userId).Include(p => p.Product).ToListAsync();
            var response = _mapper.Map<IEnumerable<ShoppingCartResponse>>(result);

            if (!response.Any())
                return new ErrorRequestDataResult<IEnumerable<ShoppingCartResponse>>(response, Messages.ShoppingCartNoRecord);

            return new SuccessRequestDataResult<IEnumerable<ShoppingCartResponse>>(response, Messages.ShoppingCartListed); ;
        }
    }
}
