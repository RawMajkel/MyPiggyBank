using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using MyPiggyBank.Core.Protocol.Resource.Mapping;
using MyPiggyBank.Core.Protocol.Resource.Requests;
using MyPiggyBank.Core.Service;
using MyPiggyBank.Data.Repository;
using Xunit;

namespace MyPiggyBank.Test.Resource
{
    public class ResourceServiceTest
    {
        private readonly IResourcesService _resourceService;
        private readonly List<Data.Model.Resource> _dbResources;
        public ResourceServiceTest()
        {
            _dbResources = new List<Data.Model.Resource>();

            var resourceRepositoryMock = CreateMockResourceRepository();
            var mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<ResourceProfile>();
            }).CreateMapper();
            _resourceService = new ResourcesService(resourceRepositoryMock.Object, mapper);
        }

        [Fact]
        public async void SaveResource_CorrectData_ShouldAddToDb()
        {
            //arrange
            var saveRequest = new ResourceSaveRequest()
            {
                Id = Guid.NewGuid(),
                Name = "TestResource",
                Currency = "$",
                Value = 4.0M
            };
            var userId = Guid.NewGuid();

            //act
            await _resourceService.SaveResource(saveRequest, userId);

            //assert\
            var entity = _dbResources.FirstOrDefault(r => r.Id == saveRequest.Id);
            Assert.NotNull(entity);
            Assert.True(entity.Name == saveRequest.Name);
            Assert.True(entity.UserId == userId);
            Assert.True(entity.Currency == saveRequest.Currency);
        }

        [Fact]
        public async void DeleteResource_ExistsInDb_ShouldRemove()
        {
            //arrange
            var dbResource = new Data.Model.Resource()
            {
                Id = Guid.NewGuid(),
            };
            _dbResources.Add(dbResource);

            //act
            await _resourceService.DeleteResource(dbResource.Id);

            //assert
            var entity = _dbResources.FirstOrDefault(r => r.Id == dbResource.Id);
            Assert.Null(entity);
        }

        private Mock<IResourcesRepository> CreateMockResourceRepository()
        {
            var resourceRepositoryMock = new Mock<IResourcesRepository>();
            resourceRepositoryMock
                .Setup(r => r.Add(It.IsAny<Data.Model.Resource>()))
                .Returns<Data.Model.Resource>((data) =>
                    Task.FromResult(Task.Run(() => _dbResources.Add(data))));

            resourceRepositoryMock
                .Setup(r => r.Get(It.IsAny<Guid>()))
                .Returns<Guid>((dataId) =>
                    Task.FromResult(_dbResources.FirstOrDefault(db => db.Id == dataId)));

            resourceRepositoryMock
                .Setup(r => r.Delete(It.IsAny<Data.Model.Resource>()))
                .Returns<Data.Model.Resource>((data) =>
                    Task.FromResult(_dbResources.Remove(data)));
            return resourceRepositoryMock;
        }
    }
}
