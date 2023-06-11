﻿using AutoMapper;
using MediatR;
using DmlFramework.Application.Features.Category.Commands;
using DmlFramework.Application.Features.Category.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DmlFramework.Application.Features.Product.Models;
using DmlFramework.Persistance.Context;

namespace DmlFramework.Application.Features.Product.Commands
{
    public class DeleteProductCommand : IRequest<ProductResponse>
    {
        public DeleteProductCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ProductResponse>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public DeleteProductCommandHandler(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ProductResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == request.Id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return _mapper.Map<ProductResponse>(product);
        }
    }
}
