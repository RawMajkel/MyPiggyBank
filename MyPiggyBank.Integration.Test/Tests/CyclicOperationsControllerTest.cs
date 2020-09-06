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
            ResourceId = _apiClient.Post("/api/v1/Resources/List", new ResourceGetRequest()).Deserialize<IList<ResourceResponse>>()[0].Id;
            OpCategoryId = _apiClient.Post("/api/v1/OperationCategories/List", new OperationCategoriesGetRequest()).Deserialize<IList<OperationCategoriesResponse>>()[0].Id;
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

            var getCyclicOperationResp = _apiClient.Post("/api/v1/CyclicOperations/List", new CyclicOperationGetRequest { Name = "TestCyclicOperation" });
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

            var getCyclicOperationsResp = _apiClient.Post("/api/v1/CyclicOperations/List", new CyclicOperationGetRequest());
            Assert.True(getCyclicOperationsResp.IsSuccessStatusCode);
            var ops = getCyclicOperationsResp.Deserialize<IList<CyclicOperationResponse>>();
            Assert.Equal(1, ops.Count);

            var deleteResp = _apiClient.Delete("/api/v1/CyclicOperations/" + ops[0].Id.ToString());
            Assert.True(deleteResp.IsSuccessStatusCode);

            getCyclicOperationsResp = _apiClient.Post("/api/v1/CyclicOperations/List", new CyclicOperationGetRequest());
            Assert.True(getCyclicOperationsResp.IsSuccessStatusCode);
            ops =  getCyclicOperationsResp.Deserialize<IList<CyclicOperationResponse>>();
            Assert.Equal(0, ops.Count);
        }

        [Fact]
        public void DeleteCyclicOperation_ShouldDeleteOnlyOne()
        {
            Assert.True(_apiClient.Post("/api/v1/CyclicOperations/Save", SampleCyclicOperation()).IsSuccessStatusCode);
            Assert.True(_apiClient.Post("/api/v1/CyclicOperations/Save", SampleCyclicOperation()).IsSuccessStatusCode);

            var getCyclicOperationsResp = _apiClient.Post("/api/v1/CyclicOperations/List", new CyclicOperationGetRequest());
            Assert.True(getCyclicOperationsResp.IsSuccessStatusCode);
            var ops = getCyclicOperationsResp.Deserialize<IList<CyclicOperationResponse>>();

            Assert.Equal(2, ops.Count);
            var guidToDelete = ops[0].Id;

            var deleteResp = _apiClient.Delete("/api/v1/CyclicOperations/" + guidToDelete.ToString());
            Assert.True(deleteResp.IsSuccessStatusCode);

            getCyclicOperationsResp = _apiClient.Post("/api/v1/CyclicOperations/List", new CyclicOperationGetRequest());
            Assert.True(getCyclicOperationsResp.IsSuccessStatusCode);
            ops =  getCyclicOperationsResp.Deserialize<IList<CyclicOperationResponse>>();

            Assert.Equal(1, ops.Count);
            Assert.True(ops[0].Id != guidToDelete);           
        }

        [Fact]
        public void GetCyclicOperation_ShouldRetrieveMultiplePresentCyclicOperationsOnUnconditionalGet()
        {
            var res = _apiClient.Post("/api/v1/CyclicOperations/Save", SampleCyclicOperation());
            Assert.True(res.IsSuccessStatusCode);

            var secondRes = SampleCyclicOperation();
            secondRes.Name = "AnotherCyclicOperation";
            Assert.True(_apiClient.Post("/api/v1/CyclicOperations/Save", secondRes).IsSuccessStatusCode);

            var getCyclicOperationsResp = _apiClient.Post("/api/v1/CyclicOperations/List", new CyclicOperationGetRequest());
            Assert.True(getCyclicOperationsResp.IsSuccessStatusCode);
            var ops = getCyclicOperationsResp.Deserialize<IList<CyclicOperationResponse>>();
            Assert.Equal(2, ops.Count);
        }

        [Fact]
        public void GetCyclicOperation_ShouldFilterByName()
        {
            Assert.True(_apiClient.Post("/api/v1/CyclicOperations/Save", SampleCyclicOperation()).IsSuccessStatusCode);
            Assert.True(_apiClient.Post("/api/v1/CyclicOperations/Save", SampleCyclicOperation()).IsSuccessStatusCode);

            var otherRes = SampleCyclicOperation();
            otherRes.Name = "AnotherCyclicOperation";
            Assert.True(_apiClient.Post("/api/v1/CyclicOperations/Save", otherRes).IsSuccessStatusCode);

            var getCyclicOperationsResp = _apiClient.Post("/api/v1/CyclicOperations/List", new CyclicOperationGetRequest { Name = "TestCyclicOperation" });
            Assert.True(getCyclicOperationsResp.IsSuccessStatusCode);
            var ops = getCyclicOperationsResp.Deserialize<IList<CyclicOperationResponse>>();
            Assert.Equal(2, ops.Count);
        }

        [Fact]
        public void GetCyclicOperation_ShouldFilterByWhetherItIsIncome()
        {
            var inputCyclicOperation = SampleCyclicOperation();
            inputCyclicOperation.IsIncome = true;
            Assert.True((_apiClient.Post("/api/v1/CyclicOperations/Save", inputCyclicOperation)).IsSuccessStatusCode);
            Assert.True((_apiClient.Post("/api/v1/CyclicOperations/Save", inputCyclicOperation)).IsSuccessStatusCode);

            inputCyclicOperation.IsIncome = false;
            Assert.True(_apiClient.Post("/api/v1/CyclicOperations/Save", inputCyclicOperation).IsSuccessStatusCode);

            var getCyclicOperationsResp = _apiClient.Post("/api/v1/CyclicOperations/List", new CyclicOperationGetRequest { IsIncome = true });
            Assert.True(getCyclicOperationsResp.IsSuccessStatusCode);
            var ops = getCyclicOperationsResp.Deserialize<IList<CyclicOperationResponse>>();
            Assert.Equal(2, ops.Count);

            getCyclicOperationsResp = _apiClient.Post("/api/v1/CyclicOperations/List", new CyclicOperationGetRequest { IsIncome = false });
            Assert.True(getCyclicOperationsResp.IsSuccessStatusCode);
            ops = getCyclicOperationsResp.Deserialize<IList<CyclicOperationResponse>>();
            Assert.Equal(1, ops.Count);
        }

        [Fact]
        public void GetCyclicOperation_ShouldFilterByEstimatedValueInequality()
        {
            Assert.True(_apiClient.Post("/api/v1/CyclicOperations/Save", SampleCyclicOperation()).IsSuccessStatusCode);
            Assert.True(_apiClient.Post("/api/v1/CyclicOperations/Save", SampleCyclicOperation()).IsSuccessStatusCode);

            var otherRes = SampleCyclicOperation();
            otherRes.EstimatedValue = 42;
            Assert.True(_apiClient.Post("/api/v1/CyclicOperations/Save", otherRes).IsSuccessStatusCode);


            var getCyclicOperationsResp = _apiClient.Post("/api/v1/CyclicOperations/List", new CyclicOperationGetRequest {MinEstimatedValue = 9000 });
            Assert.True(getCyclicOperationsResp.IsSuccessStatusCode);
            var ops = getCyclicOperationsResp.Deserialize<IList<CyclicOperationResponse>>();
            Assert.Equal(2, ops.Count);

            getCyclicOperationsResp = _apiClient.Post("/api/v1/CyclicOperations/List", new CyclicOperationGetRequest { MinEstimatedValue = 10000 });
            Assert.True(getCyclicOperationsResp.IsSuccessStatusCode);
            ops = getCyclicOperationsResp.Deserialize<IList<CyclicOperationResponse>>();
            Assert.Equal(0, ops.Count);

            getCyclicOperationsResp = _apiClient.Post("/api/v1/CyclicOperations/List", new CyclicOperationGetRequest { MaxEstimatedValue = 50 });
            Assert.True(getCyclicOperationsResp.IsSuccessStatusCode);
            ops = getCyclicOperationsResp.Deserialize<IList<CyclicOperationResponse>>();
            Assert.Equal(1, ops.Count);

            getCyclicOperationsResp = _apiClient.Post("/api/v1/CyclicOperations/List", new CyclicOperationGetRequest { MaxEstimatedValue = 10 });
            Assert.True(getCyclicOperationsResp.IsSuccessStatusCode);
            ops = getCyclicOperationsResp.Deserialize<IList<CyclicOperationResponse>>();
            Assert.Equal(0, ops.Count);
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