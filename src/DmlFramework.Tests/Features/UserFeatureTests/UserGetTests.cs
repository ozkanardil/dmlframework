using AutoMapper;
using DmlFramework.Api.Validators.UserFeatureValidators;
using DmlFramework.Application.Features.User.Commands;
using DmlFramework.Application.Features.User.Queries;
using DmlFramework.Infrastructure.Errors.Errors;
using DmlFramework.Infrastructure.Results;
using DmlFramework.Persistance.Context;
using DmlFramework.Tests.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmlFramework.Tests.Features.UserFeatureTests
{
    [Collection("UserFeatureTestsCollection")]
    public class UserGetTests : IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly GetUserByEmailQueryValidator _getUserByEmailQueryValidator;

        public UserGetTests()
        {
            _mapper = MapperBuilder.UserMapper();
            _context = new DatabaseBuilder().CreateDatabaseContext();
            _getUserByEmailQueryValidator = new GetUserByEmailQueryValidator();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("@a")]
        [InlineData("a@")]
        [InlineData("aa.com")]
        public void Get_User_By_Email_Address_Validation_Test(string email)
        {
            // Arrange
            GetUserByEmailQuery getUserQuery = new GetUserByEmailQuery();
            getUserQuery.userEmail = email;

            //Act
            bool result = _getUserByEmailQueryValidator.Validate(getUserQuery).IsValid;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Get_User_By_Email_Address_Test()
        {
            // Arrange
            GetUserByEmailQuery getUserQuery = new GetUserByEmailQuery() { userEmail = "test1@test.com" };
            var handler = new GetUserQueryHandler(_mapper, _context);

            //Act
            var result = await handler.Handle(getUserQuery, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(1, result.Data.Count());
        }

        [Fact]
        public async Task Get_User_By_Email_Address_Not_Exist_Test()
        {
            // Arrange
            GetUserByEmailQuery getUserQuery = new GetUserByEmailQuery() { userEmail = "gmail0" };
            var handler = new GetUserQueryHandler(_mapper, _context);

            //Act
            var result = await handler.Handle(getUserQuery, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(0, result.Data.Count());
        }

    }
}
