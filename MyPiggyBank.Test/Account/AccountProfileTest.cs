using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MyPiggyBank.Core.Communication.Account.Mappings;
using MyPiggyBank.Core.Communication.Account.Requests;
using MyPiggyBank.Data.Model;
using Xunit;

namespace MyPiggyBank.Test.Account
{
    public class AccountProfileTest
    {
        private readonly MapperConfiguration _mapperConfiguration;

        public AccountProfileTest()
        {
            _mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddProfile<AccountProfile>();
            });
        }

        [Fact]
        public void Profile_ShouldBeValid()
        {
            _mapperConfiguration.AssertConfigurationIsValid();
        }
    }
}
