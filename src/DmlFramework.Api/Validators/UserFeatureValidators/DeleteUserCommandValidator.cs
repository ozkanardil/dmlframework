using DmlFramework.Application.Features.User.Commands;
using DmlFramework.Application.Features.User.Constants;
using FluentValidation;

namespace DmlFramework.Api.Validators.UserFeatureValidators
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(c => c.Id).GreaterThan(0).WithMessage(UserMessages.UserIdInvalid);
        }
    }
}
