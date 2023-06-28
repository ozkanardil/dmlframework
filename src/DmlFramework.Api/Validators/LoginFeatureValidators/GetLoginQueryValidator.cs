using DmlFramework.Application.Features.Auth.Constants;
using DmlFramework.Application.Features.Auth.Queries;
using DmlFramework.Application.Shared.Constants;
using FluentValidation;

namespace DmlFramework.Api.Validators.LoginFeatureValidators
{
    public class GetLoginQueryValidator : AbstractValidator<GetLoginQuery>
    {
        public GetLoginQueryValidator()
        {
            RuleFor(c => c.Email).EmailAddress().NotNull().NotEmpty().WithMessage(SharedMassages.InvalidEmailAddress);
            RuleFor(c => c.Password).Length(4, 12).NotNull().NotEmpty().WithMessage(SharedMassages.InvalidPassword);
        }
    }
}
