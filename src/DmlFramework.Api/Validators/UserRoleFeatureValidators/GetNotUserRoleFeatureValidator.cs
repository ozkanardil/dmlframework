using DmlFramework.Application.Features.UserRole.Queries;
using DmlFramework.Application.Shared.Constants;
using FluentValidation;

namespace DmlFramework.Api.Validators.UserRoleFeatureValidators
{
    public class GetNotUserRoleFeatureValidator : AbstractValidator<GetNotUserRoleQuery>
    {
        public GetNotUserRoleFeatureValidator()
        {
            RuleFor(e => e.userId).GreaterThan(0).WithMessage(SharedMassages.InvalidId);
        }
    }
}
