using DmlFramework.Application.Features.Role.Queries;
using DmlFramework.Application.Features.User.Queries;
using DmlFramework.Application.Features.UserRole.Queries;
using DmlFramework.Application.Shared.Constants;
using FluentValidation;

namespace DmlFramework.Api.Validators.UserRoleFeatureValidators
{
    public class GetUserRoleFeatureValidator : AbstractValidator<GetUserRoleQuery>
    {
        public GetUserRoleFeatureValidator()
        {
            RuleFor(e => e.userId).GreaterThan(0).WithMessage(SharedMassages.InvalidId);
        }
    }
}
