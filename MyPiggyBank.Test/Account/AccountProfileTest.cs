using AutoMapper;
using MyPiggyBank.Core.Communication.Account.Mappings;
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
