using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Linq.Expressions;
using MyPiggyBank.Core.Communication.Account;
using MyPiggyBank.Core.Communication.Account.Mappings;
using MyPiggyBank.Core.Communication.Account.Requests;
using MyPiggyBank.Core.Services.Account.Interface;
using MyPiggyBank.Core.Services.Account.Model;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Repositories.Interfaces;
using Xunit;

namespace MyPiggyBank.Test.Account
{
    public class AccountServiceTest
    {
        private readonly IAccountService _accountService;
        private readonly List<User> _dbUsers;

        public AccountServiceTest()
        {
            _dbUsers = new List<User>()
            {
                new User()
                {
                    Id = Guid.NewGuid(),
                    Email = "test@mail.com",
                    Username = "Test",
                    PasswordHash = "PassTest"
                },
            };

            var passHasher = CreatePassHashMock();
            var userRepositoryMock = CreateMockUserRepository();
            var mapper = new MapperConfiguration(c => { c.AddProfile<AccountProfile>(); }).CreateMapper();

            _accountService = new AccountService(userRepositoryMock.Object, passHasher.Object, mapper);
        }

        [Fact]
        public async void SaveAccount_EmailExistsInDb_ShouldThrowEmailExists()
        {
            //assert
            var registerInput = new RegisterRequest()
            {
                Email = "test@mail.com",
            };

            //act
            Task act() => _accountService.SaveAccount(registerInput);

            //assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(act);
            Assert.True(exception.Message == AccountResources.AccountService_Register_Email_Exists_Error);
        }

        [Fact]
        public async void SaveAccount_UserNameExistsInDb_ShouldThrowUserNameExists()
        {
            //assert
            var registerInput = new RegisterRequest()
            {
                Email = "newTest@mail.com",
                UserName = "Test"
            };

            //act
            Task act() => _accountService.SaveAccount(registerInput);

            //assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(act);
            Assert.True(exception.Message == AccountResources.AccountService_Register_Username_Exists_Error);
        }

        [Fact]
        public async void Authenticate_CorrectLoginInput_ShouldValid()
        {
            //arrange
            var loginInput = new LoginRequest()
            {
                Email = "test@mail.com",
                Password = "PassTest",
            };
            
            //act
            var accountInfo = await _accountService.Authenticate(loginInput);

            //assert
            var entity = _dbUsers.FirstOrDefault(u => u.Email.ToLower() == loginInput.Email.ToLower());
            Assert.NotNull(accountInfo);
            Assert.True(accountInfo.Id == entity.Id);
            Assert.True(accountInfo.UserName == entity.Username);
            Assert.True(accountInfo.Email == entity.Email);
        }

        [Fact]
        public async void Authenticate_InputContainsOnlyWrongEmail_ShouldThrowUserNotFound()
        {
            //arrange
            var loginInput = new LoginRequest()
            {
                Email = "wrong@mail.com",
                Password = "PassTest",
            };

            //act
            Task act() => _accountService.Authenticate(loginInput);

            //assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(act);
            Assert.True(exception.Message == AccountResources.AccountService_Authenticate_User_NotFound);
        }

        [Fact]
        public async void Authenticate_InputContainsOnlyWrongPassword_ShouldThrowPasswordIncorrect()
        {
            //arrange
            var loginInput = new LoginRequest()
            {
                Email = "test@mail.com",
                Password = "Wrong"
            };

            //act
            Task act() => _accountService.Authenticate(loginInput);

            //assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(act);
            Assert.True(exception.Message == AccountResources.AccountService_Authenticate_Password_Incorrect);
        }

        [Fact]
        public async void Authenticate_LoginInputWrongAllProperties_ShouldThrowUserNotFound()
        {
            //arrange
            var loginInput = new LoginRequest()
            {
                Email = "wrong@mail.com",
                Password = "Wrong",
            };

            //act
            Task act() => _accountService.Authenticate(loginInput);

            //assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(act);
            Assert.True(exception.Message == AccountResources.AccountService_Authenticate_User_NotFound);
        }

        private Mock<IUserRepository> CreateMockUserRepository()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock
                .Setup(ur => ur.GetByEmail(It.IsAny<string>()))
                .Returns<string>((email) =>
                    Task.FromResult(_dbUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower())));

            userRepositoryMock
                .Setup(ur => ur.IsAny(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns<Expression<Func<User, bool>>>((predicate) =>
                    Task.FromResult(_dbUsers.Any(predicate.Compile())));

            return userRepositoryMock;
        }

        private Mock<IPasswordHasher<User>> CreatePassHashMock()
        {
            var passHasher = new Mock<IPasswordHasher<User>>();
            passHasher
                .Setup(p =>
                    p.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns<User, string, string>((u, hashPass, pass) =>
                    hashPass == pass ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed);

            return passHasher;
        }
    }
}
