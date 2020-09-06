using AutoMapper;
using MyPiggyBank.Core.Protocol.CyclicOperation.Mapping;
using MyPiggyBank.Core.Protocol.CyclicOperation.Requests;
using System;
using Xunit;

namespace MyPiggyBank.Test.CycliclOperations
{
    public class CyclicOperationProfileTest
    {
        private readonly MapperConfiguration _mapperConfiguration;

        public CyclicOperationProfileTest()
        {
            _mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddProfile<CyclicOperationProfile>();
            });
        }

        [Fact]
        public void Profile_ShouldBeValid()
        {
            _mapperConfiguration.AssertConfigurationIsValid();
        }

        [Fact]
        public void CyclicOperationIdMapping()
        {
            CyclicOperationSaveRequest test = new CyclicOperationSaveRequest { Id = Guid.NewGuid() };
            var mapper = _mapperConfiguration.CreateMapper();
            var entity = mapper.Map<Data.Model.CyclicOperation>(test);
            Assert.True(entity.Id == test.Id);
        }
    }
}
