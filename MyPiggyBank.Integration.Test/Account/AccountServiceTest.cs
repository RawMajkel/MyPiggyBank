using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyPiggyBank.Core.Communication.Account.Mappings;
using MyPiggyBank.Core.Communication.Account.Requests;
using MyPiggyBank.Core.Services.Account.Interface;
using MyPiggyBank.Core.Services.Account.Model;
using MyPiggyBank.Data;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Repositories.Models;
using Xunit;

namespace MyPiggyBank.Integration.Test.Account
{
    public class AccountServiceTest
    {
        private IAccountService _service;

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
}