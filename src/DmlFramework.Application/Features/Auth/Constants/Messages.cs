using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmlFramework.Application.Features.Auth.Constants
{
    public static class Messages
    {
        // Validation error messages
        public static string NotValidEmailAddress = "Please enter a valid email address.";
        public static string PasswordCanNotBeNullOrEmpty = "Password s required.x";
        public static string PasswordLengthIsNotValid = "Password length have to be 4-12 characters long.";

        
        public static string UserNotFound = "User not found.";
        public static string UserLoginOk = "Login succesfull.";
        public static string UserLoginErr = "Login error.";


    }
}
