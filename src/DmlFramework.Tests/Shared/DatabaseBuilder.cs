using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using DmlFramework.Domain.Entities;
using DmlFramework.Persistance.Context;

namespace DmlFramework.Tests.Shared
{
    public class DatabaseBuilder
    {

        public DatabaseContext CreateDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "test")
                .Options;

            var config = new Mock<IConfiguration>();
            config.Setup(x => x.GetSection("SomeSection")).Returns(new Mock<IConfigurationSection>().Object);

            var context = new DatabaseContext(options, config.Object);

            context.User.AddRange(new List<UserEntity>
            {
                new UserEntity { Id = 1, Name = "Name-1", Surname = "Surname-1", Email = "test1@test.com", Password="1111", Status = 1 },
                new UserEntity { Id = 2, Name = "Name-2", Surname = "Surname-2", Email = "test2@test.com", Password="2222", Status = 1 },
                new UserEntity { Id = 3, Name = "Name-3", Surname = "Surname-3", Email = "test3@test.com", Password="3333", Status = 1 },
            });

            context.Role.AddRange(new List<RoleEntity>
            {
                new RoleEntity { Id = 1, Role = "Admin" },
                new RoleEntity { Id = 2, Role = "User" },
                new RoleEntity { Id = 3, Role = "Manager" },
            });

            context.UserRole.AddRange(new List<UserRoleEntity>
            {
                new UserRoleEntity { Id = 1, UserId = 1, RoleId = 1 },
                new UserRoleEntity { Id = 2, UserId = 2, RoleId = 1 },
                new UserRoleEntity { Id = 3, UserId = 2, RoleId = 2 },
            });

            context.UserRoleV.AddRange(new List<UserRoleVEntity>
            {
                new UserRoleVEntity { Id = 1, UserId = 1, RoleId = 1, Role = "Admin" },
                new UserRoleVEntity { Id = 2, UserId = 2, RoleId = 1, Role = "Admin" },
                new UserRoleVEntity { Id = 3, UserId = 2, RoleId = 2, Role = "User" },
            });

            context.SaveChanges();

            return context;
        }



    }
}

