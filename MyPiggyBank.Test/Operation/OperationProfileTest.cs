using AutoMapper;
using MyPiggyBank.Core.Protocol.Operation.Mapping;
using MyPiggyBank.Core.Protocol.Operation.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MyPiggyBank.Test.Operation
{
    public class OperationProfileTest
    {
        private readonly MapperConfiguration _mapperConfiguration;

        public OperationProfileTest()
        {
            _mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddProfile<OperationProfile>();
            });
        }

        [Fact]
        public void Profile_ShouldBeValid()
        {
            _mapperConfiguration.AssertConfigurationIsValid();
        }
        [Fact]
        public void OperationIdMapping()
        {
            OperationSaveRequest test = new OperationSaveRequest { Id = Guid.NewGuid() };
            var mapper = _mapperConfiguration.CreateMapper();
            var entity = mapper.Map<Data.Model.Operation>(test);
            Assert.True(entity.Id == test.Id);
        }
    }
}
