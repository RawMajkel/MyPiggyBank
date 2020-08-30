using System;
using System.Collections;
using System.Collections.Generic;
using MyPiggyBank.Core.Protocol.Account.DTO;
using MyPiggyBank.Core.Protocol.Account.Requests;
using MyPiggyBank.Core.Protocol.Account;
using MyPiggyBank.Integration.Test.Responses;
using Xunit;

namespace MyPiggyBank.Integration.Test.Tests
{
    public class AccountControllerTest
    {
        private readonly RestApiClient _apiClient;
        public AccountControllerTest()
        {
            _apiClient = new RestApiClient();
        }

        [Fact]
        public void LoginUser_CorrectCredentials_ShouldCorrectResponse()
        {
            var registerResp = _apiClient.Post("/api/v1/Account/Register", new RegisterRequest() {
                Email = "someEmail@gmail.com",
                Password = "Pa$$word1",
                Username = "TestUsername"
            });
            Assert.True(registerResp.IsSuccessStatusCode);

            var loginResp = _apiClient.Post("/api/v1/Account/Login", new LoginRequest() {
                Email = "someEmail@gmail.com",
                Password = "Pa$$word1"
            });
            Assert.True(loginResp.IsSuccessStatusCode);

            var authData = loginResp.Deserialize<AuthorizationToken>();
            Assert.Equal("TestUsername", authData.Username);
            Assert.NotEmpty(authData.Token);
        }

        [Fact]
        public void LoginUser_UserNotRegistered_ShouldReturnUserNotFound()
        {
            var loginResp = _apiClient.Post("/api/v1/Account/Login", new LoginRequest() {
                Email = "_definifively_not_registered_@gmail.com",
                Password = "Pa$$word1"
            });
            Assert.False(loginResp.IsSuccessStatusCode);
            Assert.Equal(AccountResources.AccountService_Authenticate_User_NotFound, loginResp.Deserialize<string>());
        }

        [Theory]
        [InlineData("WrongPass")]
        public void LoginUser_IncorrectPassword_ShouldReturnValidationMessage(string wrongPassword)
        {
            var registerResp = _apiClient.Post("/api/v1/Account/Register", new RegisterRequest() {
                Email = "email@gmail.com",
                Password = "Pa$$word1",
                Username = "TestUser"
            });

            var loginResp = _apiClient.Post("/api/v1/Account/Login", new LoginRequest() {
                Email = "email@gmail.com",
                Password = wrongPassword
            });
            Assert.False(loginResp.IsSuccessStatusCode);
            Assert.Equal(AccountResources.AccountService_Authenticate_Password_Incorrect, loginResp.Deserialize<String>());
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

        [Theory, ClassData(typeof(IncorrectPasswordData))]
        public void RegisterUser_IncorrectPassword_ShouldReturnValidationMessage(string incorrectPassword, string validationMessage)
        {
            var response = _apiClient.Post("/api/v1/Account/Register", new RegisterRequest() {
                Email = "test@gmail.com",
                Password = incorrectPassword,
                Username = "testUser"
            });
            Assert.False(response.IsSuccessStatusCode);
            
            var validation = response.Deserialize<FluentValidationResponse>();
            Assert.True(validation.Errors.ContainsKey("Password"));
            Assert.Contains(validationMessage, validation.Errors["Password"]);
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