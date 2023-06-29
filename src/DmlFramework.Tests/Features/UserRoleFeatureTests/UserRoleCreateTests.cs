using AutoMapper;
using DmlFramework.Api.Validators.UserFeatureValidators;
using DmlFramework.Api.Validators.UserRoleFeatureValidators;
using DmlFramework.Application.Features.User.Commands;
using DmlFramework.Application.Features.UserRole.Command;
using DmlFramework.Persistance.Context;
using DmlFramework.Tests.Shared;

namespace DmlFramework.Tests.Features.UserFeatureTests
{
    [Collection("UserFeatureTestsCollection")]
    public class UserRoleCreateTests: IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly CreateUserRoleCommandValidator _createUserRoleCommandValidator;

        public UserRoleCreateTests()
        {
            _mapper = MapperBuilder.UserRoleMapper();
            _context = new DatabaseBuilder().CreateDatabaseContext();
            _createUserRoleCommandValidator = new CreateUserRoleCommandValidator();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Theory]
        [InlineData(0, "test@test.com")]
        [InlineData(-1, "test@test.com")]
        [InlineData(-20, "test@test.com")]
        [InlineData(5, "test5")]
        [InlineData(5, "test@")]
        [InlineData(5, "test.com")]
        [InlineData(5, ".com")]
        public void UserRole_Create_Validation_Test(int roleId, string email)
        {
            // Arrange
            CreateUserRoleCommand createUserRoleCommand = new CreateUserRoleCommand();
            createUserRoleCommand.userRoleId = roleId;
            createUserRoleCommand.userEmail = email;

            //Act
            bool result = _createUserRoleCommandValidator.Validate(createUserRoleCommand).IsValid;

            // Assert
            Assert.False(result);
        }


        [Theory]
        [InlineData(3, "test3@test.com")]
        public async Task UserRole_Create_Test(int roleId, string userEmail)
        {
            // Arrange
            CreateUserRoleCommand createUserRoleCommand = new CreateUserRoleCommand();
            createUserRoleCommand.userRoleId = roleId;
            createUserRoleCommand.userEmail = userEmail;

            var handler = new CreateUserRoleCommandHandler(_mapper, _context);

            //Act
            var result = await handler.Handle(createUserRoleCommand, CancellationToken.None);
            var resultUserCount = _context.UserRole.ToList().Count;

            // Assert
            Assert.True(result.Success);
            Assert.Equal(4, resultUserCount);
        }
    }
}
