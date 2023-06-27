using DmlFramework.Application.Features.Auth.Queries;
using DmlFramework.Application.Features.User.Commands;
using DmlFramework.Application.Features.User.Constants;
using DmlFramework.Application.Shared.Constants;
using FluentValidation;

namespace DmlFramework.Api.Validators.UserFeatureValidators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage(UserMessages.UserNameInvalid);
            RuleFor(c => c.Surname).NotEmpty().WithMessage(UserMessages.UserSurnameInvalid);
            RuleFor(c => c.Email).EmailAddress().NotNull().NotEmpty().WithMessage(SharedMassages.InvalidEmailAddress);
            RuleFor(c => c.Password).Length(4, 12).NotNull().NotEmpty().WithMessage(SharedMassages.InvalidPassword);
        }
    }
}
