﻿using System;
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
            Assert.True(_apiClient.Post("/api/v1/Resources/Save", SampleResource()).IsSuccessStatusCode);

            var secondRes = SampleResource();
            secondRes.Name = "AnotherResource";
            Assert.True(_apiClient.Post("/api/v1/Resources/Save", secondRes).IsSuccessStatusCode);
        }

        [Fact]
        public void CreateResource_ShouldPostCorrectData()
        {
            Assert.True(_apiClient.Post("/api/v1/Resources/Save", SampleResource()).IsSuccessStatusCode);

            var getResourceResp = _apiClient.Get("/api/v1/Resources/List?Name=TestResource");
            Assert.True(getResourceResp.IsSuccessStatusCode);

            var resources = getResourceResp.Deserialize<IList<ResourceResponse>>();
            Assert.Equal(1, resources.Count);
            Assert.NotEqual(Guid.Empty, resources[0].Id);
            Assert.True(resources[0].Value > 9000);
        }

        [Fact]
        public void DeleteResource_ShouldDeleteFromDB()
        {
            Assert.True(_apiClient.Post("/api/v1/Resources/Save", SampleResource()).IsSuccessStatusCode);

            var getResourcesResp = _apiClient.Get("/api/v1/Resources/List");
            Assert.True(getResourcesResp.IsSuccessStatusCode);
            var resources = getResourcesResp.Deserialize<IList<ResourceResponse>>();
            Assert.Equal(1, resources.Count);

            var deleteResp = _apiClient.Delete("/api/v1/Resources/Delete/" + resources[0].Id.ToString());
            Assert.True(deleteResp.IsSuccessStatusCode);

            getResourcesResp = _apiClient.Get("/api/v1/Resources/List");
            Assert.True(getResourcesResp.IsSuccessStatusCode);
            resources = getResourcesResp.Deserialize<IList<ResourceResponse>>();
            Assert.Equal(0, resources.Count);
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