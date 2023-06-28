using AutoMapper;
using DmlFramework.Api.Validators.UserFeatureValidators;
using DmlFramework.Application.Features.Role.Queries;
using DmlFramework.Application.Features.User.Queries;
using DmlFramework.Persistance.Context;
using DmlFramework.Tests.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmlFramework.Tests.Features.RoleFeatureTests
{
    [Collection("UserFeatureTestsCollection")]
    public class RoleGetTests : IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public RoleGetTests()
        {
            _mapper = MapperBuilder.RoleMapper();
            _context = new DatabaseBuilder().CreateDatabaseContext();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task Get_Roles_Test()
        {
            // Arrange
            GetRoleQuery getRoleQuery = new GetRoleQuery();
            var handler = new GetRoleQueryHandler(_mapper, _context);

            //Act
            var result = await handler.Handle(getRoleQuery, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(2, result.Data.Count());
        }
    }
}
