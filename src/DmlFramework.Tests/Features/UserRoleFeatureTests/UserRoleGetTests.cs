using AutoMapper;
using DmlFramework.Api.Validators.UserFeatureValidators;
using DmlFramework.Api.Validators.UserRoleFeatureValidators;
using DmlFramework.Application.Features.User.Commands;
using DmlFramework.Application.Features.User.Queries;
using DmlFramework.Application.Features.UserRole.Queries;
using DmlFramework.Infrastructure.Errors.Errors;
using DmlFramework.Infrastructure.Results;
using DmlFramework.Persistance.Context;
using DmlFramework.Tests.Shared;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmlFramework.Tests.Features.UserRoleFeatureTests
{
    [Collection("UserFeatureTestsCollection")]
    public class UserRoleGetTests : IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly GetUserRoleFeatureValidator _getUserRoleFeatureValidator;

        public UserRoleGetTests()
        {
            _mapper = MapperBuilder.UserRoleMapper();
            _context = new DatabaseBuilder().CreateDatabaseContext();
            _getUserRoleFeatureValidator = new GetUserRoleFeatureValidator();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public void Get_User_Role_By_UserId_Validation_Test(int userId)
        {
            // Arrange
            GetUserRoleQuery getUserRoleQuery = new GetUserRoleQuery();
            getUserRoleQuery.userId = userId;

            //Act
            bool result = _getUserRoleFeatureValidator.Validate(getUserRoleQuery).IsValid;

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public async Task Get_User_Role_By_UserId_Test(int userId, int expectedRecordCount)
        {
            // Arrange
            GetUserRoleQuery getUserRoleQuery = new GetUserRoleQuery();
            getUserRoleQuery.userId = userId;

            var handler = new GetUserRoleQueryHandler(_mapper, _context);

            //Act
            var result = await handler.Handle(getUserRoleQuery, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(expectedRecordCount, result.Data.Count());
        }

        [Fact]
        public async Task Get_User_By_Email_Address_Not_Exist_Test()
        {
            // Arrange
            GetUserRoleQuery getUserRoleQuery = new GetUserRoleQuery();
            getUserRoleQuery.userId = 3;

            var handler = new GetUserRoleQueryHandler(_mapper, _context);

            //Act
            var result = await handler.Handle(getUserRoleQuery, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Empty(result.Data);
        }

    }
}
