using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using DmlFramework.Domain.Entities;
using DmlFramework.Persistance.Context;

namespace DmlFramework.Tests.Shared
{
    public class DynamicDatabaseBuilder
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
                new UserEntity { Id = 1, Name = "Name-1", Surname = "Surname-1", Email = "gmail1", Password="111", Status = 1 },
                new UserEntity { Id = 2, Name = "Name-2", Surname = "Surname-2", Email = "gmail2", Password="222", Status = 1 },
                new UserEntity { Id = 3, Name = "Name-3", Surname = "Surname-3", Email = "gmail3", Password="333", Status = 1 },
            });

            context.SaveChanges();

            return context;
        }



    }
}

