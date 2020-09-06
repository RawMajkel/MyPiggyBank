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

            var getCyclicOperationResp = _apiClient.Post("/api/v1/OperationCategories/List", new OperationCategoriesGetRequest { Name = "TestOpCategory" });
            Assert.True(getCyclicOperationResp.IsSuccessStatusCode);

            var ops = getCyclicOperationResp.Deserialize<IList<OperationCategoriesResponse>>();
            Assert.Equal(1, ops.Count);
            Assert.NotEqual(Guid.Empty, ops[0].Id);
            Assert.Equal("TestOpCategory", ops[0].Name);
        }

        [Fact]
        public void DeleteCyclicOperation_ShouldDeleteFromDB()
        {
            Assert.True(_apiClient.Post("/api/v1/OperationCategories/Save", SampleOperationCategory()).IsSuccessStatusCode);

            var getOperationCategoriesResp = _apiClient.Post("/api/v1/OperationCategories/List", new OperationCategoriesGetRequest());
            Assert.True(getOperationCategoriesResp.IsSuccessStatusCode);
            var ops = getOperationCategoriesResp.Deserialize<IList<OperationCategoriesResponse>>();
            Assert.Equal(1, ops.Count);

            var deleteResp = _apiClient.Delete("/api/v1/OperationCategories/" + ops[0].Id.ToString());
            Assert.True(deleteResp.IsSuccessStatusCode);

            getOperationCategoriesResp = _apiClient.Post("/api/v1/OperationCategories/List", new OperationCategoriesGetRequest());
            Assert.True(getOperationCategoriesResp.IsSuccessStatusCode);
            ops =  getOperationCategoriesResp.Deserialize<IList<OperationCategoriesResponse>>();
            Assert.Equal(0, ops.Count);
        }

        [Fact]
        public void DeleteCyclicOperation_ShouldDeleteOnlyOne()
        {
            Assert.True(_apiClient.Post("/api/v1/OperationCategories/Save", SampleOperationCategory()).IsSuccessStatusCode);
            Assert.True(_apiClient.Post("/api/v1/OperationCategories/Save", SampleOperationCategory()).IsSuccessStatusCode);

            var getOperationCategoriesResp = _apiClient.Post("/api/v1/OperationCategories/List", new OperationCategoriesGetRequest());
            Assert.True(getOperationCategoriesResp.IsSuccessStatusCode);
            var ops = getOperationCategoriesResp.Deserialize<IList<OperationCategoriesResponse>>();

            Assert.Equal(2, ops.Count);
            var guidToDelete = ops[0].Id;

            var deleteResp = _apiClient.Delete("/api/v1/OperationCategories/" + guidToDelete.ToString());
            Assert.True(deleteResp.IsSuccessStatusCode);

            getOperationCategoriesResp = _apiClient.Post("/api/v1/OperationCategories/List", new OperationCategoriesGetRequest());
            Assert.True(getOperationCategoriesResp.IsSuccessStatusCode);
            ops =  getOperationCategoriesResp.Deserialize<IList<OperationCategoriesResponse>>();

            Assert.Equal(1, ops.Count);
            Assert.True(ops[0].Id != guidToDelete);           
        }

        [Fact]
        public void GetCyclicOperation_ShouldRetrieveMultiplePresentOperationCategoriesOnUnconditionalGet()
        {
            var op = _apiClient.Post("/api/v1/OperationCategories/Save", SampleOperationCategory());
            Assert.True(op.IsSuccessStatusCode);

            var secondOp = SampleOperationCategory();
            secondOp.Name = "AnotherCategory";
            Assert.True(_apiClient.Post("/api/v1/OperationCategories/Save", secondOp).IsSuccessStatusCode);

            var getOperationCategoriesResp = _apiClient.Post("/api/v1/OperationCategories/List", new OperationCategoriesGetRequest());
            Assert.True(getOperationCategoriesResp.IsSuccessStatusCode);
            var ops = getOperationCategoriesResp.Deserialize<IList<OperationCategoriesResponse>>();
            Assert.Equal(2, ops.Count);
        }

        [Fact]
        public void GetCyclicOperation_ShouldFilterByName()
        {
            Assert.True(_apiClient.Post("/api/v1/OperationCategories/Save", SampleOperationCategory()).IsSuccessStatusCode);
            Assert.True(_apiClient.Post("/api/v1/OperationCategories/Save", SampleOperationCategory()).IsSuccessStatusCode);

            var otherOp = SampleOperationCategory();
            otherOp.Name = "AnotherCategory";
            Assert.True(_apiClient.Post("/api/v1/OperationCategories/Save", otherOp).IsSuccessStatusCode);

            var getOperationCategoriesResp = _apiClient.Post("/api/v1/OperationCategories/List", new OperationCategoriesGetRequest { Name = "TestOpCategory" });
            Assert.True(getOperationCategoriesResp.IsSuccessStatusCode);
            var ops = getOperationCategoriesResp.Deserialize<IList<OperationCategoriesResponse>>();
            Assert.Equal(2, ops.Count);
        }

        private OperationCategoriesSaveRequest SampleOperationCategory() => new OperationCategoriesSaveRequest() {
            Name = "TestOpCategory"
        };
    }
}