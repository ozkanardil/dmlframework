using DmlFramework.Infrastructure.Errors;
using DmlFramework.Infrastructure.Errors.Errors;
using DmlFramework.Application.Features.User.Commands;
using DmlFramework.Application.Features.User.Constants;

namespace DmlFramework.Application.Features.User.Rules
{
    public static class GuardUserCreate
    {
        public static GuardUserCreateClause Against(CreateUserCommand value)
        {
            return new GuardUserCreateClause(value);
        }
    }

    public class GuardUserCreateClause
    {
        private readonly CreateUserCommand _value;

        public GuardUserCreateClause(CreateUserCommand value)
        {
            _value = value;
        }

        public GuardUserCreateClause Null()
        {
            if (_value.Name == null || _value.Surname == null || _value.Email == null || _value.Password == null)
                throw new CustomException(UserMessages.UserNameInvalid, false);

            return this;
        }

       
        // Add more methods here for other guard clauses...

        public void KeepGoing()
        {

        }
        //public CreateUserCommand Value => _value;
    }

}
