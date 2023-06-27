using DmlFramework.Application.Features.Auth.Queries;
using DmlFramework.Application.Features.User.Queries;
using DmlFramework.Application.Shared.Constants;
using FluentValidation;

namespace DmlFramework.Api.Validators.UserFeatureValidators
{
    public class GetUserByEmailQueryValidator : AbstractValidator<GetUserByEmailQuery>
    {
        public GetUserByEmailQueryValidator()
        {
            RuleFor(e => e.userEmail).NotNull().NotEmpty().EmailAddress().WithMessage(SharedMassages.InvalidEmailAddress);
        }
    }
}
