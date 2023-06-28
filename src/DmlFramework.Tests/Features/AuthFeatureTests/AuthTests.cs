using AutoMapper;
using DmlFramework.Api.Validators.LoginFeatureValidators;
using DmlFramework.Api.Validators.UserFeatureValidators;
using DmlFramework.Application.Features.Auth.Queries;
using DmlFramework.Application.Features.User.Commands;
using DmlFramework.Infrastructure.Security.JwtToken;
using DmlFramework.Persistance.Context;
using DmlFramework.Tests.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmlFramework.Tests.Features.AuthFeatureTests
{
    [Collection("UserFeatureTestsCollection")]
    public class AuthTests : IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly GetLoginQueryValidator _getLoginQueryValidator;
        private ITokenHelper _tokenHelper;

        public AuthTests()
        {
            _mapper = MapperBuilder.AuthMapper();
            _context = new DatabaseBuilder().CreateDatabaseContext();
            _getLoginQueryValidator = new GetLoginQueryValidator();
            _tokenHelper = new TokenBuilder();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Theory]
        [InlineData("", "1234")]
        [InlineData("a", "1234")]
        [InlineData("a.com", "1234")]
        [InlineData("a.net", "1234")]
        [InlineData("test@test.com", "")]
        [InlineData("test@test.com", "1")]
        [InlineData("test@test.com", "12")]
        [InlineData("test@test.com", "123")]
        public void Login_Validation_Test(string email, string password)
        {
            // Arrange
            GetLoginQuery getLoginQuery = new GetLoginQuery();
            getLoginQuery.Email = email;
            getLoginQuery.Password = password;

            //Act
            bool result = _getLoginQueryValidator.Validate(getLoginQuery).IsValid;

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("test1@test.com", "1111")]
        [InlineData("test2@test.com", "2222")]
        [InlineData("test3@test.com", "3333")]
        public async Task Login_Success_Test(string userEmail, string userPassword)
        {
            // Arrange
            GetLoginQuery getLoginQuery = new GetLoginQuery();
            getLoginQuery.Email = userEmail;
            getLoginQuery.Password = userPassword;

            var handler = new GetLoginQueryHandler(_mapper, _context, _tokenHelper);

            //Act
            var result = await handler.Handle(getLoginQuery, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(TestConstants.Token, result.Data.Token.Token);
        }

        [Theory]
        [InlineData("test@test.com", "1111")]
        [InlineData("test1@test.com", "1112")]
        [InlineData("test1@test.com", "11111")]
        public async Task Login_Fail_Test(string userEmail, string userPassword)
        {
            // Arrange
            GetLoginQuery getLoginQuery = new GetLoginQuery();
            getLoginQuery.Email = userEmail;
            getLoginQuery.Password = userPassword;

            var handler = new GetLoginQueryHandler(_mapper, _context, _tokenHelper);

            //Act
            var result = await handler.Handle(getLoginQuery, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
        }
    }
}
