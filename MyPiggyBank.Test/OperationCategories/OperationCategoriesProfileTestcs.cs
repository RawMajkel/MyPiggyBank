using AutoMapper;
using MyPiggyBank.Core.Protocol.OperationCategories.Mapping;
using MyPiggyBank.Core.Protocol.OperationCategories.Requests;
using System;
using Xunit;

namespace MyPiggyBank.Test.OperationCategories
{
    public class OperationCategoriesProfileTest
    {
        private readonly MapperConfiguration _mapperConfiguration;

        public OperationCategoriesProfileTest()
        {
            _mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddProfile<OperationCategoriesProfile>();
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
            OperationCategoriesSaveRequest test = new OperationCategoriesSaveRequest { Id = Guid.NewGuid() };
            var mapper = _mapperConfiguration.CreateMapper();
            var entity = mapper.Map<Data.Model.OperationCategory>(test);
            Assert.True(entity.Id == test.Id);
        }
    }
}
