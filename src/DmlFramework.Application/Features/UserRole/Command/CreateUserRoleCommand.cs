using AutoMapper;
using MediatR;
using DmlFramework.Application.Features.User.Models;
using DmlFramework.Application.Features.UserRole.Constant;
using DmlFramework.Domain.Entities;
using DmlFramework.Infrastructure.Results;
using DmlFramework.Persistance.Context;

namespace DmlFramework.Application.Features.UserRole.Command
{
    public class CreateUserRoleCommand : IRequest<IRequestResult>
    {
        public string userEmail { get; set; }
        public int userRoleId { get; set; }
    }

    public class CreateUserRoleCommandHandler : IRequestHandler<CreateUserRoleCommand, IRequestResult>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public CreateUserRoleCommandHandler(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<IRequestResult> Handle(CreateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = _context.User.Where(u => u.Email == request.userEmail).FirstOrDefault();
            UserRoleEntity roleEntity = new UserRoleEntity();
            roleEntity.RoleId = request.userRoleId;
            roleEntity.UserId = user.Id;

            _context.UserRole.Add(roleEntity);
            int numAffectedRecords = await _context.SaveChangesAsync(cancellationToken);

            if (numAffectedRecords == 0)
                return new ErrorRequestResult(Messages.UserRoleAddError);

            return new SuccessRequestResult(Messages.UserRoleAddSuccess);
        }
    }
}
