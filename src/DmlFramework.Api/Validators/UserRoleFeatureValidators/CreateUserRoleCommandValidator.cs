using DmlFramework.Application.Features.User.Commands;
using DmlFramework.Application.Features.User.Constants;
using DmlFramework.Application.Features.UserRole.Command;
using DmlFramework.Application.Shared.Constants;
using FluentValidation;

namespace DmlFramework.Api.Validators.UserRoleFeatureValidators
{
    public class CreateUserRoleCommandValidator : AbstractValidator<CreateUserRoleCommand>
    {
        public CreateUserRoleCommandValidator()
        {
            RuleFor(c => c.userRoleId).GreaterThan(0).WithMessage(SharedMassages.InvalidId);
            RuleFor(c => c.userEmail).EmailAddress().NotNull().NotEmpty().WithMessage(SharedMassages.InvalidEmailAddress);
        }
    }
}
