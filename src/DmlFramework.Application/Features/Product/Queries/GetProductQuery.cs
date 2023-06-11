using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DmlFramework.Application.Features.Category.Models;
using DmlFramework.Application.Features.Category.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DmlFramework.Application.Features.Product.Models;
using DmlFramework.Persistance.Context;

namespace DmlFramework.Application.Features.Product.Queries
{
    public class GetProductQuery : IRequest<IEnumerable<ProductResponse>>
    {

    }

    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, IEnumerable<ProductResponse>>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<ProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var result = _context.Products.AsQueryable();
            var response = await result.ToListAsync();
            return _mapper.Map<IEnumerable<ProductResponse>>(response).ToList();

        }
    }
}
