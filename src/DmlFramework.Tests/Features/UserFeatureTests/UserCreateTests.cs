﻿using AutoMapper;
using DmlFramework.Api.Validators.UserFeatureValidators;
using DmlFramework.Application.Features.User.Commands;
using DmlFramework.Application.Features.User.Constants;
using DmlFramework.Application.Features.User.Queries;
using DmlFramework.Application.Shared.Constants;
using DmlFramework.Infrastructure.Errors.Errors;
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
    public class UserCreateTests: IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly CreateUserCommandValidator _createUserCommandValidator;

        public UserCreateTests()
        {
            _mapper = MapperBuilder.UserMapper();
            _context = new DatabaseBuilder().CreateDatabaseContext();
            _createUserCommandValidator = new CreateUserCommandValidator();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Theory]
        [InlineData(4, "", "TesSurname", "test@test.com", "", 1)]
        [InlineData(4, "TestName", "", "test@test.com", "", 1)]
        [InlineData(4, "TestName", "TesSurname", "", "1234", 1)]
        [InlineData(4, "TestName", "TesSurname", "a", "1234", 1)]
        [InlineData(4, "TestName", "TesSurname", "test@test.com", "", 1)]
        [InlineData(4, "TestName", "TesSurname", "test@test.com", "1", 1)]
        [InlineData(4, "TestName", "TesSurname", "test@test.com", "12", 1)]
        [InlineData(4, "TestName", "TesSurname", "test@test.com", "123", 1)]
        public void User_Create_Validation_Test(int id, string name, string surName, string email, string password, int status)
        {
            // Arrange
            CreateUserCommand createUserCommand = new CreateUserCommand();
            createUserCommand.Id = id;
            createUserCommand.Name = name;
            createUserCommand.Surname = surName;
            createUserCommand.Email = email;
            createUserCommand.Status = status;

            //Act
            bool result = _createUserCommandValidator.Validate(createUserCommand).IsValid;

            // Assert
            Assert.False(result);
        }


        [Theory]
        [InlineData(4, "TestName", "TesSurname", "test@test.com", "1234", 1)]
        public async Task User_Create_Test(int userId, string userName, string userSurname, string userEmail, string userPassword, int userStatus)
        {
            // Arrange
            CreateUserCommand createUserCommand = new CreateUserCommand();
            createUserCommand.Id = userId;
            createUserCommand.Name = userName;
            createUserCommand.Surname = userSurname;
            createUserCommand.Email = userEmail;
            createUserCommand.Password = userPassword;
            createUserCommand.Status = userStatus;

            var handler = new CreateUserCommandHandler(_mapper, _context);

            //Act
            var result = await handler.Handle(createUserCommand, CancellationToken.None);
            var resultUserCount = _context.User.ToList().Count;

            // Assert
            Assert.True(result.Success);
            Assert.Equal(4, resultUserCount);
        }

        [Theory]
        [InlineData(1, "Name-1", "Surname-1", "test1@test.com", "1111", 1)]
        [InlineData(2, "Name-2", "Surname-2", "test2@test.com", "2222", 1)]
        public async Task User_Create_Existance_Check_Test(int userId, string userName, string userSurname, string userEmail, string userPassword, int userStatus)
        {
            // Arrange
            CreateUserCommand createUserCommand = new CreateUserCommand();
            createUserCommand.Id = userId;
            createUserCommand.Name = userName;
            createUserCommand.Surname = userSurname;
            createUserCommand.Email = userEmail;
            createUserCommand.Password = userPassword;
            createUserCommand.Status = userStatus;

            var handler = new CreateUserCommandHandler(_mapper, _context);

            //Act
            Func<Task> act = () => handler.Handle(createUserCommand, CancellationToken.None);
            var exception = await Assert.ThrowsAsync<CustomException>(act);

            // Assert
            Assert.Equal(UserMessages.UserAlreadyExist, exception.Message);
        }
    }
}
