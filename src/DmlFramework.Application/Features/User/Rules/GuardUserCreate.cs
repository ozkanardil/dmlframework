using DmlFramework.Infrastructure.Errors;
using DmlFramework.Infrastructure.Errors.Errors;
using DmlFramework.Application.Features.User.Commands;
using DmlFramework.Application.Features.User.Constants;
using DmlFramework.Persistance.Context;
using Newtonsoft.Json.Linq;

namespace DmlFramework.Application.Features.User.Rules
{
    public static class GuardUserCreate
    {
        public static GuardUserCreateClause Against(CreateUserCommand value, DatabaseContext context)
        {
            return new GuardUserCreateClause(value, context);
        }
    }

    public class GuardUserCreateClause
    {
        private readonly CreateUserCommand _value;
        private readonly DatabaseContext _context;


        public GuardUserCreateClause(CreateUserCommand value, DatabaseContext context)
        {
            _value = value;
            _context = context;
        }

        public GuardUserCreateClause UserAlreadyExist()
        {
            var result = _context.User.Where(u=>u.Email == _value.Email).ToList();
            if (result.Count > 0)
                throw new CustomException(UserMessages.UserAlreadyExist, false);

            return this;
        }

       
        // Add more methods here for other guard clauses...

        public void KeepGoing()
        {

        }
        //public CreateUserCommand Value => _value;
    }

}
