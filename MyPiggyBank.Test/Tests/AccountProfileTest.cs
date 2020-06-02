using AutoMapper;
using MyPiggyBank.Core.Protocol;
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
                c.AddProfile<AccountsProfile>();
            });
        }

        [Fact]
        public void Profile_ShouldBeValid()
        {
            _mapperConfiguration.AssertConfigurationIsValid();
        }
    }
}
