using DmlFramework.Application.Features.Auth.Constants;
using DmlFramework.Application.Features.Auth.Queries;
using FluentValidation;

namespace DmlFramework.Api.Validators
{
    public class GetLoginQueryValidator : AbstractValidator<GetLoginQuery>
    {
        public GetLoginQueryValidator()
        {
            RuleFor(l => l.Email).NotNull().NotEmpty().EmailAddress().WithMessage("vv");
            RuleFor(l => l.Password).NotNull().NotEmpty().Length(4, 12).WithMessage(Messages.PasswordCanNotBeNullOrEmpty);
        }
    }
}
