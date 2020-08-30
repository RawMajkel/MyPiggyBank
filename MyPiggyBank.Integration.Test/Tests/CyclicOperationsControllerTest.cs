using MyPiggyBank.Core.Protocol.CyclicOperation.Requests;
using MyPiggyBank.Core.Protocol.CyclicOperation.Responses;
using MyPiggyBank.Core.Protocol.OperationCategories.Requests;
using MyPiggyBank.Core.Protocol.OperationCategories.Responses;
using MyPiggyBank.Core.Protocol.Resource.Requests;
using MyPiggyBank.Core.Protocol.Resource.Responses;
using System;
using System.Collections.Generic;
using Xunit;

namespace MyPiggyBank.Integration.Test.Tests
{
    public class CyclicOperationsControllerTest {
        private RestApiClient _apiClient;
        private Guid ResourceId;
        private Guid OpCategoryId;

        public CyclicOperationsControllerTest()
        {
            _apiClient = new RestApiClient();
            _apiClient.TestUserAuth();

            _apiClient.Post("/api/v1/Resources/Save", SampleResource());
            _apiClient.Post("/api/v1/OperationCategories/Save", SampleOperationCategory());
            ResourceId = _apiClient.Get("/api/v1/Resources/List").Deserialize<IList<ResourceResponse>>()[0].Id;
            OpCategoryId = _apiClient.Get("/api/v1/OperationCategories/List").Deserialize<IList<OperationCategoriesResponse>>()[0].Id;
        }

        [Fact]
        public void CreateCyclicOperation_ShouldSuccessfullyPost()
        {
            Assert.True(_apiClient.Post("/api/v1/CyclicOperations/Save", SampleCyclicOperation()).IsSuccessStatusCode);
        }

        [Fact]
        public void CreateCyclicOperation_ShouldSuccessfullyPostMultipleTimes()
        {
            Assert.True(_apiClient.Post("/api/v1/CyclicOperations/Save", SampleCyclicOperation()).IsSuccessStatusCode);

            var secondRes = SampleCyclicOperation();
            secondRes.Name = "AnotherCyclicOperation";
            Assert.True(_apiClient.Post("/api/v1/CyclicOperations/Save", secondRes).IsSuccessStatusCode);
        }

        [Fact]
        public void CreateCyclicOperation_ShouldPostCorrectData()
        {
            Assert.True(_apiClient.Post("/api/v1/CyclicOperations/Save", SampleCyclicOperation()).IsSuccessStatusCode);

            var getCyclicOperationResp = _apiClient.Get("/api/v1/CyclicOperations/List?Name=TestCyclicOperation");
            Assert.True(getCyclicOperationResp.IsSuccessStatusCode);

            var ops = getCyclicOperationResp.Deserialize<IList<CyclicOperationResponse>>();
            Assert.Equal(1, ops.Count);
            Assert.NotEqual(Guid.Empty, ops[0].Id);
            Assert.True(ops[0].EstimatedValue > 9000);
        }

        [Fact]
        public void DeleteCyclicOperation_ShouldDeleteFromDB()
        {
            Assert.True(_apiClient.Post("/api/v1/CyclicOperations/Save", SampleCyclicOperation()).IsSuccessStatusCode);

            var getCyclicOperationsResp = _apiClient.Get("/api/v1/CyclicOperations/List");
            Assert.True(getCyclicOperationsResp.IsSuccessStatusCode);
            var ops = getCyclicOperationsResp.Deserialize<IList<CyclicOperationResponse>>();
            Assert.Equal(1, ops.Count);

            var deleteResp = _apiClient.Delete("/api/v1/CyclicOperations/Delete/" + ops[0].Id.ToString());
            Assert.True(deleteResp.IsSuccessStatusCode);

            getCyclicOperationsResp = _apiClient.Get("/api/v1/CyclicOperations/List");
            Assert.True(getCyclicOperationsResp.IsSuccessStatusCode);
            ops =  getCyclicOperationsResp.Deserialize<IList<CyclicOperationResponse>>();
            Assert.Equal(0, ops.Count);
        }

        [Fact]
        public void DeleteCyclicOperation_ShouldDeleteOnlyOne()
        {
        
        }

        [Fact]
        public void GetCyclicOperation_ShouldRetrieveMultiplePresentCyclicOperationsOnUnconditionalGet()
        {

        }

        [Fact]
        public void GetCyclicOperation_ShouldFilterByName()
        {

        }

        [Fact]
        public void GetCyclicOperation_ShouldFilterByWhetherItIsIncome()
        {

        }

        [Fact]
        public void GetCyclicOperation_ShouldFilterByEstimatedValueInequality()
        {

        }

        private CyclicOperationSaveRequest SampleCyclicOperation() => new CyclicOperationSaveRequest()
        {
            Name = "TestCyclicOperation",
            EstimatedValue = 9000.01M,
            IsIncome = false,
            NextOccurence = DateTime.Now,
            ResourceId = ResourceId,
            OperationCategoryId = OpCategoryId
        };

        private ResourceSaveRequest SampleResource() => new ResourceSaveRequest() {
            Name = "TestResource",
            Value = 9000.01M,
            Currency = "USD"
        };

        private OperationCategoriesSaveRequest SampleOperationCategory() => new OperationCategoriesSaveRequest() {
            Name = "TestOpCategory"
        };
    }
}