using AutoMapper;
using MediatR;
using DmlFramework.Application.Features.User.Constants;
using DmlFramework.Infrastructure.Results;
using DmlFramework.Persistance.Context;

namespace DmlFramework.Application.Features.User.Commands
{
    public class DeleteUserCommand : IRequest<IRequestResult>
    {
        public int Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, IRequestResult>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public DeleteUserCommandHandler(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IRequestResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = _context.User.SingleOrDefault(u => u.Id == request.Id);

            if (user == null)
                return new ErrorRequestResult(Messages.UserNotFound);

            user.Status = 0;
            _context.User.Update(user);
            int numAffectedRecords = await _context.SaveChangesAsync(cancellationToken);

            if (numAffectedRecords == 0)
                return new ErrorRequestResult(Messages.UserDeleteError);

            return new SuccessRequestResult(Messages.UserDeleted);

        }
    }
}
