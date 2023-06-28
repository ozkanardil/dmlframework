using DmlFramework.Domain.Entities;
using DmlFramework.Infrastructure.Security.JwtToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmlFramework.Tests.Shared
{
    public class TokenBuilder: ITokenHelper
    {
        public AccessToken CreateToken(UserEntity user, List<UserRoleDto> userClaims)
        {
            AccessToken tokenForTest = new AccessToken();
            tokenForTest.Token = TestConstants.Token;
            tokenForTest.Expiration = DateTime.Today.AddDays(1);

            return tokenForTest;
        }
    }
}
