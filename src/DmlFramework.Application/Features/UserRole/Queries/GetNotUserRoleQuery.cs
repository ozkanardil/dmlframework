using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DmlFramework.Infrastructure.Results;
using DmlFramework.Application.Features.Role.Models;
using DmlFramework.Application.Features.Role.Constants;
using DmlFramework.Persistance.Context;

namespace DmlFramework.Application.Features.UserRole.Queries
{
    public class GetNotUserRoleQuery : IRequest<IRequestDataResult<IEnumerable<RoleResponse>>>
    {
        public int userId { get; set; }
    }

    public class GetNotUserRoleQueryHandler : IRequestHandler<GetNotUserRoleQuery, IRequestDataResult<IEnumerable<RoleResponse>>>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public GetNotUserRoleQueryHandler(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<IRequestDataResult<IEnumerable<RoleResponse>>> Handle(GetNotUserRoleQuery request, CancellationToken cancellationToken)
        {
            var selectedUser = await _context.User.FirstOrDefaultAsync(u => u.Id == request.userId);

            if (selectedUser == null)
                return new SuccessRequestDataResult<IEnumerable<RoleResponse>>(null, Messages.RolesListError);

            var userResult = from userOperationClaim in _context.UserRoleV
                             where userOperationClaim.UserId == request.userId
                             select userOperationClaim.RoleId;

            var result = from OperationClaim in _context.Role
                         where !userResult.Contains(OperationClaim.Id)
                         select new RoleResponse
                         {
                             Id = OperationClaim.Id,
                             Name = OperationClaim.Role,
                         };

            var response = _mapper.Map<IEnumerable<RoleResponse>>(result.ToList());

            if (!response.Any())
                return new ErrorRequestDataResult<IEnumerable<RoleResponse>>(response, Messages.RolesListError);

            return new SuccessRequestDataResult<IEnumerable<RoleResponse>>(response, Messages.RolesListed); ;
        }
    }
}
