using AutoMapper;
using MediatR;
using DmlFramework.Application.Features.User.Constants;
using DmlFramework.Application.Features.User.Models;
using DmlFramework.Application.Features.User.Rules;
using DmlFramework.Domain.Entities;
using DmlFramework.Infrastructure.Results;
using DmlFramework.Persistance.Context;

namespace DmlFramework.Application.Features.User.Commands
{
    public class CreateUserCommand : UserModel, IRequest<IRequestResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Status { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IRequestResult>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IMapper mapper,
                                        DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<IRequestResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            GuardUserCreate.Against(request)
                .Null()
                .KeepGoing();


            var user = _mapper.Map<UserEntity>(request);
            user.Status = 1;
            _context.User.Add(user);
            int numAffectedRecords = await _context.SaveChangesAsync(cancellationToken);

            if (numAffectedRecords == 0)
                return new ErrorRequestResult(UserMessages.UserAddError);

            return new SuccessRequestResult(UserMessages.UserAddSuccess);
        }
    }
}
