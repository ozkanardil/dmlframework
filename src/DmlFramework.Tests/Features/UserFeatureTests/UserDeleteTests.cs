using AutoMapper;
using DmlFramework.Api.Validators.UserFeatureValidators;
using DmlFramework.Application.Features.User.Commands;
using DmlFramework.Persistance.Context;
using DmlFramework.Tests.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmlFramework.Tests.Features.UserFeatureTests
{
    [Collection("UserFeatureTestsCollection")]
    public class UserDeleteTests : IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly DeleteUserCommandValidator _deleteUserCommandValidator;

        public UserDeleteTests()
        {
            _mapper = MapperBuilder.UserMapper();
            _context = new DatabaseBuilder().CreateDatabaseContext();
            _deleteUserCommandValidator = new DeleteUserCommandValidator();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void User_Delete_Validation_Test(int id)
        {
            // Arrange
            DeleteUserCommand deleteUserCommand = new DeleteUserCommand();
            deleteUserCommand.Id = id;

            //Act
            bool result = _deleteUserCommandValidator.Validate(deleteUserCommand).IsValid;

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task User_Delete_Test(int id)
        {
            // Arrange
            DeleteUserCommand deleteUserCommand = new DeleteUserCommand();
            deleteUserCommand.Id = id;

            var handler = new DeleteUserCommandHandler(_mapper, _context);

            //Act
            bool resultValidation = _deleteUserCommandValidator.Validate(deleteUserCommand).IsValid;

            var resultDelete = await handler.Handle(deleteUserCommand, CancellationToken.None);
            
            var resultDeletedUserCount = _context.User.Where(u=>u.Status == 0).ToList().Count;

            // Assert
            Assert.True(resultValidation);
            Assert.True(resultDelete.Success);
            Assert.Equal(1, resultDeletedUserCount);
        }
    }
}
