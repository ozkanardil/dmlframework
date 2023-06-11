using AutoMapper;
using MediatR;
using DmlFramework.Application.Features.Product.Commands;
using DmlFramework.Application.Features.Product.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                return new ErrorRequestResult(Messages.UserAddError);

            return new SuccessRequestResult(Messages.UserAddSuccess);
        }
    }
}
