using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DmlFramework.Infrastructure.Results;
using DmlFramework.Application.Features.User.Constants;
using DmlFramework.Application.Features.User.Models;
using DmlFramework.Infrastructure.Results;
using DmlFramework.Persistance.Context;

namespace DmlFramework.Application.Features.User.Queries
{
    public class GetUserByEmailQuery : IRequest<IRequestDataResult<IEnumerable<UserResponse>>>
    {
        public string userEmail { get; set; }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserByEmailQuery, IRequestDataResult<IEnumerable<UserResponse>>>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<IRequestDataResult<IEnumerable<UserResponse>>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.User.Where(u => u.Status == 1 && u.Email == request.userEmail).ToListAsync();
            var response = _mapper.Map<IEnumerable<UserResponse>>(result);

            if (!response.Any())
                return new ErrorRequestDataResult<IEnumerable<UserResponse>>(response, UserMessages.UserNoRecord);

            return new SuccessRequestDataResult<IEnumerable<UserResponse>>(response, UserMessages.UserListed); ;
        }
    }
}
