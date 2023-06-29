using DmlFramework.Application.Features.UserRole.Command;
using DmlFramework.Application.Shared.Constants;
using FluentValidation;

namespace DmlFramework.Api.Validators.UserRoleFeatureValidators
{
    public class DeleteUserRoleCommandValidator : AbstractValidator<DeleteUserRoleCommand>
    {
        public DeleteUserRoleCommandValidator()
        {
            RuleFor(c => c.Id).GreaterThan(0).WithMessage(SharedMassages.InvalidId);
        }
    }
}
