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
            RuleFor(l => l.Email).NotNull().NotEmpty().EmailAddress().WithMessage(SharedMassages.InvalidEmailAddress);
            RuleFor(l => l.Password).NotNull().NotEmpty().Length(4, 12).WithMessage(Messages.PasswordCanNotBeNullOrEmpty);
        }
    }
}
