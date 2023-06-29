using AutoMapper;
using DmlFramework.Api.Validators.UserFeatureValidators;
using DmlFramework.Api.Validators.UserRoleFeatureValidators;
using DmlFramework.Application.Features.User.Commands;
using DmlFramework.Application.Features.UserRole.Command;
using DmlFramework.Persistance.Context;
using DmlFramework.Tests.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmlFramework.Tests.Features.UserRoleFeatureTests
{
    [Collection("UserFeatureTestsCollection")]
    public class UserRoleDeleteTests : IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly DeleteUserRoleCommandValidator _deleteUserRoleCommandValidator;

        public UserRoleDeleteTests()
        {
            _mapper = MapperBuilder.UserRoleMapper();
            _context = new DatabaseBuilder().CreateDatabaseContext();
            _deleteUserRoleCommandValidator = new DeleteUserRoleCommandValidator();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void UserRole_Delete_Validation_Test(int id)
        {
            // Arrange
            DeleteUserRoleCommand deleteUserRoleCommand = new DeleteUserRoleCommand();
            deleteUserRoleCommand.Id = id;

            //Act
            bool result = _deleteUserRoleCommandValidator.Validate(deleteUserRoleCommand).IsValid;

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(1, 1, 0)]
        [InlineData(2, 2, 1)]
        public async Task UserRole_Delete_Test(int userRoleId, int userId, int expectedUserRoleCount)
        {
            // Arrange
            DeleteUserRoleCommand deleteUserRoleCommand = new DeleteUserRoleCommand();
            deleteUserRoleCommand.Id = userRoleId;

            var handler = new DeleteUserRoleCommandHandler(_mapper, _context);

            //Act
            bool resultValidation = _deleteUserRoleCommandValidator.Validate(deleteUserRoleCommand).IsValid;

            var resultDelete = await handler.Handle(deleteUserRoleCommand, CancellationToken.None);

            var resultDeletedUserCount = _context.UserRole.Where(u => u.UserId == userId).ToList().Count;

            // Assert
            Assert.True(resultValidation);
            Assert.True(resultDelete.Success);
            Assert.Equal(expectedUserRoleCount, resultDeletedUserCount);
        }

    }
}
