using MyPiggyBank.Core.Protocol.OperationCategories.Requests;
using MyPiggyBank.Core.Protocol.OperationCategories.Responses;
using System;
using System.Collections.Generic;
using Xunit;

namespace MyPiggyBank.Integration.Test.Tests
{
    public class OperationCategoriesControllerTest {
        private RestApiClient _apiClient;

        public OperationCategoriesControllerTest()
        {
            _apiClient = new RestApiClient();
            _apiClient.TestUserAuth();
        }

        [Fact]
        public void CreateCyclicOperation_ShouldSuccessfullyPost()
        {
            Assert.True(_apiClient.Post("/api/v1/OperationCategories/Save", SampleOperationCategory()).IsSuccessStatusCode);
        }

        [Fact]
        public void CreateCyclicOperation_ShouldSuccessfullyPostMultipleTimes()
        {
            Assert.True(_apiClient.Post("/api/v1/OperationCategories/Save", SampleOperationCategory()).IsSuccessStatusCode);

            var secondRes = SampleOperationCategory();
            secondRes.Name = "AnotherCyclicOperation";
            Assert.True(_apiClient.Post("/api/v1/OperationCategories/Save", secondRes).IsSuccessStatusCode);
        }

        [Fact]
        public void CreateCyclicOperation_ShouldPostCorrectData()
        {
            Assert.True(_apiClient.Post("/api/v1/OperationCategories/Save", SampleOperationCategory()).IsSuccessStatusCode);

            var getCyclicOperationResp = _apiClient.Get("/api/v1/OperationCategories/List?Name=TestOpCategory");
            Assert.True(getCyclicOperationResp.IsSuccessStatusCode);

            var ops = getCyclicOperationResp.Deserialize<IList<OperationCategoriesResponse>>();
            Assert.Equal(1, ops.Count);
            Assert.NotEqual(Guid.Empty, ops[0].Id);
            Assert.Equal("TestOpCategory", ops[0].Name);
        }

        [Fact]
        public void DeleteCyclicOperation_ShouldDeleteFromDB()
        {

        }

        [Fact]
        public void DeleteCyclicOperation_ShouldDeleteOnlyOne()
        {
         
        }

        [Fact]
        public void GetCyclicOperation_ShouldRetrieveMultiplePresentOperationCategoriesOnUnconditionalGet()
        {

        }

        [Fact]
        public void GetCyclicOperation_ShouldFilterByName()
        {

        }

        private OperationCategoriesSaveRequest SampleOperationCategory() => new OperationCategoriesSaveRequest() {
            Name = "TestOpCategory"
        };
    }
}