using AutoMapper;
using MediatR;
using DmlFramework.Application.Features.Category.Commands;
using DmlFramework.Application.Features.Category.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DmlFramework.Application.Features.Product.Models;
using DmlFramework.Domain.Entities;
using DmlFramework.Persistance.Context;

namespace DmlFramework.Application.Features.Product.Commands
{
    public class UpdateProductCommand : ProductModel, IRequest<ProductResponse>
    {
        public int Id { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResponse>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<ProductEntity>(request);
            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<ProductResponse>(product);
        }
    }
}
