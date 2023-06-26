using AutoMapper;
using DmlFramework.Application.Features.User.Queries;
using DmlFramework.Infrastructure.Results;
using DmlFramework.Persistance.Context;
using DmlFramework.Tests.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmlFramework.Tests.Features
{
    public class UserTests : IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public UserTests()
        {
            _mapper = MapperBuilder.UserMapper();
            _context = new DatabaseBuilder().CreateDatabaseContext();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task User_By_Email_Address_Test()
        {
            // Arrange
            GetUserByEmailQuery getUserQuery = new GetUserByEmailQuery() { userEmail = "gmail1"};
            var handler = new GetUserQueryHandler(_mapper, _context);

            //Act
            var result = await handler.Handle(getUserQuery, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(1, result.Data.Count());
        }

        [Fact]
        public async Task User_By_Email_Address_Not_Exist_Test()
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
