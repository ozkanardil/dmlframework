using DmlFramework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmlFramework.Infrastructure.Security.JwtToken
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(UserEntity user, List<UserRoleDto> userClaims);
    }
}
