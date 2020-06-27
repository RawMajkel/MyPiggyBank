using AutoMapper;
using MyPiggyBank.Core.Protocol.CyclicOperation.Mapping;
using Xunit;

namespace MyPiggyBank.Test.CycliclOperations
{
    public class CycliclOperationProfleTest
    {
        private readonly MapperConfiguration _mapperConfiguration;

        public CycliclOperationProfleTest()
        {
            _mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddProfile<CyclicOperationRequestProfile>();
                c.AddProfile<CyclicOperationResponseProfile>();
            });
        }

        [Fact]
        public void Profile_ShouldBeValid()
        {
            _mapperConfiguration.AssertConfigurationIsValid();
        }
    }
}
