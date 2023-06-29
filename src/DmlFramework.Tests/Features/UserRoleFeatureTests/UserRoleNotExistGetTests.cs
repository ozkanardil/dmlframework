using AutoMapper;
using DmlFramework.Api.Validators.UserRoleFeatureValidators;
using DmlFramework.Application.Features.UserRole.Queries;
using DmlFramework.Persistance.Context;
using DmlFramework.Tests.Shared;

namespace DmlFramework.Tests.Features.UserRoleFeatureTests
{
    [Collection("UserFeatureTestsCollection")]
    public class UserRoleNotExistGetTests : IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly GetNotUserRoleFeatureValidator _getNotUserRoleFeatureValidator;

        public UserRoleNotExistGetTests()
        {
            _mapper = MapperBuilder.UserRoleMapper();
            _context = new DatabaseBuilder().CreateDatabaseContext();
            _getNotUserRoleFeatureValidator = new GetNotUserRoleFeatureValidator();
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
        public void Get_Not_User_Role_By_UserId_Validation_Test(int userId)
        {
            // Arrange
            GetNotUserRoleQuery getNotUserRoleQuery = new GetNotUserRoleQuery();
            getNotUserRoleQuery.userId = userId;

            //Act
            bool result = _getNotUserRoleFeatureValidator.Validate(getNotUserRoleQuery).IsValid;

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(3, 3)]
        public async Task Get_User_Role_By_UserId_Test(int userId, int expectedRecordCount)
        {
            // Arrange
            GetNotUserRoleQuery getNotUserRoleQuery = new GetNotUserRoleQuery();
            getNotUserRoleQuery.userId = userId;

            var handler = new GetNotUserRoleQueryHandler(_mapper, _context);

            //Act
            var result = await handler.Handle(getNotUserRoleQuery, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(expectedRecordCount, result.Data.Count());
        }

    }
}
