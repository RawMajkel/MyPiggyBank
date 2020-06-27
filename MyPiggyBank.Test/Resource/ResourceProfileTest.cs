using System;
using AutoMapper;
using Xunit;
using MyPiggyBank.Core.Protocol.Resource.Mapping;
using MyPiggyBank.Core.Protocol.Resource.Requests;

namespace MyPiggyBank.Test.Resource
{
    public class ResourceProfileTest
    {
        private readonly MapperConfiguration _mapperConfiguration;

        public ResourceProfileTest()
        {
            _mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddProfile<ResourceProfile>();
            });
        }

        [Fact]
        public void Profile_ShouldBeValid()
        {
            _mapperConfiguration.AssertConfigurationIsValid();
        }
        [Fact]
        public void ResourceIdMapping()
        {
            ResourceSaveRequest test = new ResourceSaveRequest { Id = Guid.NewGuid() };
            var mapper = _mapperConfiguration.CreateMapper();
            var entity = mapper.Map<Data.Model.Resource>(test);
            Assert.True(entity.Id == test.Id);
        }
    }
}
