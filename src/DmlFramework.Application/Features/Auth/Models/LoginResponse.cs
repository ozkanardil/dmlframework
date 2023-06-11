using DmlFramework.Application.Features.UserRole.Models;
using DmlFramework.Infrastructure.Security.JwtToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmlFramework.Application.Features.Auth.Models
{
    public class LoginResponse
    {
        public TokenResult Token { get; set; }
        public List<UserRoleResponse> Roles { get; set; }
    }

    public class TokenResult : AccessToken
    {
        public string refreshToken { get; set; }
    }

}
