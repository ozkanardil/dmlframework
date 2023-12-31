﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DmlFramework.Infrastructure.Results;
using DmlFramework.Application.Features.UserRole.Models;
using DmlFramework.Application.Features.UserRole.Constant;
using DmlFramework.Infrastructure.Results;
using DmlFramework.Persistance.Context;

namespace DmlFramework.Application.Features.UserRole.Queries
{
    public class GetUserRoleQuery : IRequest<IRequestDataResult<IEnumerable<UserRoleResponse>>>
    {
        public int userId { get; set; }
    }

    public class GetUserRoleQueryHandler : IRequestHandler<GetUserRoleQuery, IRequestDataResult<IEnumerable<UserRoleResponse>>>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public GetUserRoleQueryHandler(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<IRequestDataResult<IEnumerable<UserRoleResponse>>> Handle(GetUserRoleQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.UserRoleV.Where(ur => ur.UserId == request.userId).ToListAsync();
            var response = _mapper.Map<IEnumerable<UserRoleResponse>>(result);

            if (!response.Any())
                return new ErrorRequestDataResult<IEnumerable<UserRoleResponse>>(response, Messages.UserRoleNoRecord);

            return new SuccessRequestDataResult<IEnumerable<UserRoleResponse>>(response, Messages.UserRolesListed); ;
        }
    }
}
