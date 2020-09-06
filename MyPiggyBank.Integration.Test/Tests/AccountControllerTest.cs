using System;
using System.Collections;
using System.Collections.Generic;
using MyPiggyBank.Core.Protocol.Account.DTO;
using MyPiggyBank.Core.Protocol.Account.Requests;
using MyPiggyBank.Core.Protocol.Account;
using MyPiggyBank.Integration.Test.Responses;
using Xunit;
using MyPiggyBank.Core.Service;
using MyPiggyBank.Data;
using AutoMapper;
using MyPiggyBank.Data.Repository;
using Microsoft.AspNetCore.Identity;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Core.Protocol.Account.Mappings;
using System.Linq;

namespace MyPiggyBank.Integration.Test.Tests
{
    public class AccountControllerTest
    {
        private IAccountsService _service;
        private readonly RestApiClient _apiClient;
        public AccountControllerTest()
        {
            _apiClient = new RestApiClient();
        }

        [Fact]
        public void LoginUser_CorrectCredentials_ShouldCorrectResponse()
        {
            //arrange
            var registerInput = new RegisterRequest() {
                Email = "someEmail@gmail.com",
                Password = "Pa$$word1",
                Username = "TestUsername"
            };
            var loginInput = new LoginRequest() {
                Email = "someEmail@gmail.com",
                Password = "Pa$$word1"
            };
            var dateTimeNow = DateTime.Now;

            //act
            _apiClient.Post("/api/v1/Account/Register", registerInput);      
            var response = _apiClient.Post("/api/v1/Account/Login", loginInput);           
            var authToken = response.Deserialize<AuthorizationToken>();

            //assert
            Assert.NotNull(authToken);
            Assert.True(authToken.Identifier == loginInput.Email);
            Assert.True(authToken.Expiration != DateTime.MaxValue && authToken.Expiration != DateTime.MinValue);
            Assert.True(authToken.Expiration > dateTimeNow && authToken.Expiration < dateTimeNow.AddMinutes(25));
            Assert.True(!string.IsNullOrEmpty(authToken.Token));
        }

        [Fact]
        public void LoginUser_UserNotRegistered_ShouldReturnUserNotFound()
        {
            //arrange
            var loginInput = new LoginRequest() {
                Email = "_definifively_not_registered_@gmail.com",
                Password = "Pa$$word1"
            };

            //act
            var response = _apiClient.Post("/api/v1/Account/Login", loginInput);
            var message = response.Deserialize<string>();

            //assert
            Assert.NotEmpty(message);
            Assert.Equal(AccountResources.AccountService_Authenticate_User_NotFound, message);
        }

        [Theory]
        [InlineData("WrongPass", "Incorrect password.")]
        public void LoginUser_IncorrectPassword_ShouldReturnValidationMessage(string wrongPassword, string validationMessage)
        {
            // arrange
            var registerInput = new RegisterRequest() {
                Email = "email@gmail.com",
                Password = "Pa$$word1",
                Username = "TestUser"
            };
            var loginInput = new LoginRequest() {
                Email = "email@gmail.com",
                Password = wrongPassword
            };

            //act
            _apiClient.Post("/api/v1/Account/Register", registerInput);
            var response = _apiClient.Post("/api/v1/Account/Login", loginInput);
            var message = response.Deserialize<string>();

            //assert
            Assert.True(!string.IsNullOrEmpty(message));
            Assert.Equal(validationMessage, message);
        }

        [Theory, ClassData(typeof(IncorrectPasswordData))]
        public void RegisterUser_IncorrectPassword_ShouldReturnValidationMessage(
            string incorrectPassword, string validationMessage)
        {
            //arrange
            var registerInput = new RegisterRequest() {
                Email = "test@gmail.com",
                Password = incorrectPassword,
                Username = "testUser"
            };

            //act
            var response = _apiClient.Post("/api/v1/Account/Register", registerInput);
            var validation = response.Deserialize<FluentValidationResponse>();

            //assert
            Assert.True(validation.Errors.ContainsKey("Password"));
            Assert.Contains(validationMessage, validation.Errors["Password"]);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void LoginUser_MalformedPassword_ShouldReturnValidationMessage(string malformedPassword) {
            _apiClient.Post("/api/v1/Account/Register", new RegisterRequest() {
                Email = "email@gmail.com",
                Password = "Pa$$word1",
                Username = "TestUser"
            });

            var loginResp = _apiClient.Post("/api/v1/Account/Login", new LoginRequest() {
                Email = "email@gmail.com",
                Password = malformedPassword
            });
            Assert.False(loginResp.IsSuccessStatusCode);

            var validation = loginResp.Deserialize<FluentValidationResponse>();
            Assert.True(validation.Errors.ContainsKey("Password"));
        }

        [Fact]
        public async void SaveAccount_SavingCorrectUser_ShouldAddToDb()
        {
            //arrange
            using var context = DbHelper.CreateDbInRuntimeMemory();
            _service = CreateAccountService(context);
            var user = new RegisterRequest() {
                Email = "tesMail@gmail.com",
                Password = "Pass1",
                Username = "User1"
            };

            //act
            await _service.SaveAccount(user);

            //assert
            var entity = context.Users.FirstOrDefault();

            Assert.NotNull(entity);
            Assert.NotEqual(Guid.Empty, entity.Id);
            Assert.True(entity.Email == user.Email);
            Assert.True(entity.Username == user.Username);
            Assert.NotNull(entity.PasswordHash);
        }
        private IAccountsService CreateAccountService(MyPiggyBankContext context)
        {
            var mappingConf = new MapperConfiguration(mc => { mc.AddProfile<AccountProfile>(); });
            IMapper mapper = mappingConf.CreateMapper();

            var userRepository = new UsersRepository(context);

            return new AccountsService(
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