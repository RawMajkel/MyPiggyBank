﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyPiggyBank.Core.Communication.Account;
using MyPiggyBank.Core.Communication.Account.DTO;
using MyPiggyBank.Core.Communication.Account.Mappings;
using MyPiggyBank.Core.Communication.Account.Requests;
using MyPiggyBank.Core.Services.Account.Interface;
using MyPiggyBank.Core.Services.Account.Model;
using MyPiggyBank.Data;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Repositories.Models;
using MyPiggyBank.Integration.Test.Responses;
using Xunit;

namespace MyPiggyBank.Integration.Test.Account
{
    public class AccountServiceTest
    {
        private IAccountService _service;
        private readonly RestApiClient _apiClient;

        public AccountServiceTest()
        {
            _apiClient = new RestApiClient();
        }

        [Fact]
        public async void LoginUser_CorrectCredentials_ShouldCorrectResponse()
        {
            //arrange
            var registerInput = new RegisterRequest()
            {
                Email = "email@gmail.com",
                Password = "Pa$$word1",
                UserName = "TestUser"
            };
            var loginInput = new LoginRequest()
            {
                Email = "email@gmail.com",
                Password = "Pa$$word1"
            };
            var dateTimeNow = DateTime.Now;

            //act
            await _apiClient.PostAsync("/api/v1/Account/Register", registerInput);
            var response = await _apiClient.PostAsync("/api/v1/Account/Login", loginInput);
            var authToken = response.Deserialize<AuthorizationToken>();

            //assert
            Assert.NotNull(authToken);
            Assert.True(authToken.Identifier == loginInput.Email);
            Assert.True(authToken.Expiration != DateTime.MaxValue && authToken.Expiration != DateTime.MinValue);
            Assert.True(authToken.Expiration > dateTimeNow && authToken.Expiration < dateTimeNow.AddMinutes(25));
            Assert.True(!string.IsNullOrEmpty(authToken.Token));
        }

        [Fact]
        public async void LoginUser_UserNotRegistered_ShouldReturnUserNotFound()
        {
            //arrange
            var loginInput = new LoginRequest()
            {
                Email = "email@gmail.com",
                Password = "Pa$$word1"
            };

            //act
            var response = await _apiClient.PostAsync("/api/v1/Account/Login", loginInput);
            var message = response.Deserialize<string>();

            //assert
            Assert.NotEmpty(message);
            Assert.True(message == AccountResources.AccountService_Authenticate_User_NotFound);
        }

        [Theory]
        [InlineData("WrongPass", "Incorrect password.")]
        public async void LoginUser_IncorrectPassword_ShouldReturnValidationMessage(string wrongPassword, string validationMessage)
        {
            // arrange
            var registerInput = new RegisterRequest()
            {
                Email = "email@gmail.com",
                Password = "Pa$$word1",
                UserName = "TestUser"
            };
            var loginInput = new LoginRequest()
            {
                Email = "email@gmail.com",
                Password = wrongPassword
            };

            //act
            await _apiClient.PostAsync("/api/v1/Account/Register", registerInput);
            var response = await _apiClient.PostAsync("/api/v1/Account/Login", loginInput);
            var message = response.Deserialize<string>();

            //assert
            Assert.True(!string.IsNullOrEmpty(message));
            Assert.True(validationMessage == message);
        }

        [Theory, ClassData(typeof(IncorrectPasswordData))]
        public async void RegisterUser_IncorrectPassword_ShouldReturnValidationMessage(string incorrectPassword,
            string validationMessage)
        {
            //arrange
            var registerInput = new RegisterRequest()
            {
                Email = "test@gmail.com",
                Password = incorrectPassword,
                UserName = "testUser"
            };

            //act
            var response = await _apiClient.PostAsync("/api/v1/Account/Register", registerInput);
            var validation = response.Deserialize<FluentValidationResponse>();

            //assert
            Assert.True(validation.Errors.ContainsKey("Password"));
            Assert.True(validation.Errors["Password"].Any(message => message == validationMessage));
        }

        [Fact]
        public async void SaveAccount_SavingCorrectUser_ShouldAddToDb()
        {
            //arrange
            using var context = DbHelper.CreateDbInRuntimeMemory();
            _service = CreateAccountService(context);
            var user = new RegisterRequest()
            {
                Email = "tesMail@gmail.com",
                Password = "Pass1",
                UserName = "User1"
            };

            //act
            await _service.SaveAccount(user);

            //assert
            var entity = context.Users.FirstOrDefault();

            Assert.NotNull(entity);
            Assert.NotEqual(Guid.Empty, entity.Id);
            Assert.True(entity.Email == user.Email);
            Assert.True(entity.Username == user.UserName);
            Assert.NotNull(entity.PasswordHash);
        }

        private IAccountService CreateAccountService(MyPiggyBankContext context)
        {
            var mappingConf = new MapperConfiguration(mc => { mc.AddProfile<AccountProfile>(); });
            IMapper mapper = mappingConf.CreateMapper();

            var userRepository = new UserRepository(context);

            return new AccountService(
                repository: userRepository,
                hasher: new PasswordHasher<User>(),
                mapper: mapper);
        }
    }

    public class IncorrectPasswordData : IEnumerable<object[]>
    {
        private readonly List<object[]> _passes = new List<object[]>()
        {
            new object[] { string.Empty, AccountResources.RegisterRequestValidator_Password_Empty_Error },
            new object[] { "pas", AccountResources.RegisterRequestValidator_Password_Length_Error },
            new object[] { "pa$$word1", AccountResources.RegisterRequestValidator_Password_UpperCaseLetter_Error },
            new object[] { "Pa$$word", AccountResources.RegisterRequestValidator_Password_Digit_Error },
            new object[] { "Password1", AccountResources.RegisterRequestValidator_Password_SpecialCharacter_Error },
        };

        public IEnumerator<object[]> GetEnumerator() => _passes.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}