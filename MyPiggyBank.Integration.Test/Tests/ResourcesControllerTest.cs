using System;
using System.Collections.Generic;
using MyPiggyBank.Core.Protocol.Resource.Requests;
using MyPiggyBank.Core.Protocol.Resource.Responses;
using Xunit;

namespace MyPiggyBank.Integration.Test.Tests
{
    public class ResourcesControllerTest {
        private RestApiClient _apiClient;

        public ResourcesControllerTest()
        {
            _apiClient = new RestApiClient();
            _apiClient.TestUserAuth();
        }

        [Fact]
        public void CreateResource_ShouldSuccessfullyPost()
            => Assert.True(_apiClient.Post("/api/v1/Resources/Save", SampleResource()).IsSuccessStatusCode);        

        [Fact]
        public void CreateResource_ShouldSuccessfullyPostMultipleTimes()
        {

        }

        [Fact]
        public void CreateResource_ShouldPostCorrectData()
        {

        }

        [Fact]
        public void DeleteResource_ShouldDeleteFromDB()
        {

        }

        [Fact]
        public void DeleteResource_ShouldDeleteOnlyOne()
        {
     
        }

        [Fact]
        public void GetResource_ShouldRetrieveMultiplePresentResourcesOnUnconditionalGet()
        {

        }

        [Fact]
        public void GetResource_ShouldFilterByName()
        {

        }

        [Fact]
        public void GetResource_ShouldFilterByCurrency()
        {

        }

        [Fact]
        public void GetResource_ShouldFilterByValueInequality()
        {

        }

        private ResourceSaveRequest SampleResource() => new ResourceSaveRequest()
        {
            Name = "TestResource",
            Value = 9000.01M,
            Currency = "USD"
        };
    }
}