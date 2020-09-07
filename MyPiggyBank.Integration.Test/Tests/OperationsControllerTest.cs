using MyPiggyBank.Core.Protocol.Operation.Requests;
using MyPiggyBank.Core.Protocol.Operation.Responses;
using MyPiggyBank.Core.Protocol.OperationCategories.Requests;
using MyPiggyBank.Core.Protocol.OperationCategories.Responses;
using MyPiggyBank.Core.Protocol.Resource.Requests;
using MyPiggyBank.Core.Protocol.Resource.Responses;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Integration.Test.Responses;
using System;
using System.Collections.Generic;
using Xunit;

namespace MyPiggyBank.Integration.Test.Tests
{
    public class OperationsControllerTest {
        private RestApiClient _apiClient;
        private Guid ResourceId;
        private Guid OpCategoryId;

        public OperationsControllerTest()
        {
            _apiClient = new RestApiClient();
            _apiClient.TestUserAuth();

            _apiClient.Post("/api/v1/Resources/Save", SampleResource());
            _apiClient.Post("/api/v1/OperationCategories/Save", SampleOperationCategory());
            ResourceId = _apiClient.Post("/api/v1/Resources/List", new ResourceGetRequest()).Deserialize<IList<ResourceResponse>>()[0].Id;
            OpCategoryId = _apiClient.Post("/api/v1/OperationCategories/List", new OperationCategoriesGetRequest()).Deserialize<IList<OperationCategoriesResponse>>()[0].Id;
        }

        [Fact]
        public void CreateOperation_ShouldSuccessfullyPost()
        {
            Assert.True(_apiClient.Post("/api/v1/Operations/Save", SampleOperation()).IsSuccessStatusCode);
        }

        [Fact]
        public void CreateOperation_ShouldSuccessfullyPostMultipleTimes()
        {
            Assert.True(_apiClient.Post("/api/v1/Operations/Save", SampleOperation()).IsSuccessStatusCode);

            var secondRes = SampleOperation();
            secondRes.Name = "AnotherOperation";
            Assert.True(_apiClient.Post("/api/v1/Operations/Save", secondRes).IsSuccessStatusCode);
        }

        [Fact]
        public void CreateOperation_ShouldPostCorrectData()
        {
            Assert.True(_apiClient.Post("/api/v1/Operations/Save", SampleOperation()).IsSuccessStatusCode);

            var getOperationResp = _apiClient.Post("/api/v1/Operations/List", new OperationGetRequest { Name = "TestOperation" });
            Assert.True(getOperationResp.IsSuccessStatusCode);

            var ops = getOperationResp.Deserialize<IList<OperationResponse>>();
            Assert.Equal(1, ops.Count);
            Assert.NotEqual(Guid.Empty, ops[0].Id);
            Assert.True(ops[0].Value > 9000);
        }

        [Fact]
        public void DeleteOperation_ShouldDeleteFromDB()
        {
            Assert.True(_apiClient.Post("/api/v1/Operations/Save", SampleOperation()).IsSuccessStatusCode);

            var getOperationsResp = _apiClient.Post("/api/v1/Operations/List", new OperationGetRequest());
            Assert.True(getOperationsResp.IsSuccessStatusCode);
            var ops = getOperationsResp.Deserialize<IList<OperationResponse>>();
            Assert.Equal(1, ops.Count);

            var deleteResp = _apiClient.Delete("/api/v1/Operations/" + ops[0].Id.ToString());
            Assert.True(deleteResp.IsSuccessStatusCode);

            getOperationsResp = _apiClient.Post("/api/v1/Operations/List", new OperationGetRequest());
            Assert.True(getOperationsResp.IsSuccessStatusCode);
            ops =  getOperationsResp.Deserialize<IList<OperationResponse>>();
            Assert.Equal(0, ops.Count);
        }

        [Fact]
        public void DeleteOperation_ShouldDeleteOnlyOne()
        {
            Assert.True(_apiClient.Post("/api/v1/Operations/Save", SampleOperation()).IsSuccessStatusCode);
            Assert.True(_apiClient.Post("/api/v1/Operations/Save", SampleOperation()).IsSuccessStatusCode);

            var getOperationsResp = _apiClient.Post("/api/v1/Operations/List", new OperationGetRequest());
            Assert.True(getOperationsResp.IsSuccessStatusCode);
            var ops = getOperationsResp.Deserialize<IList<OperationResponse>>();

            Assert.Equal(2, ops.Count);
            var guidToDelete = ops[0].Id;

            var deleteResp = _apiClient.Delete("/api/v1/Operations/" + guidToDelete.ToString());
            Assert.True(deleteResp.IsSuccessStatusCode);

            getOperationsResp = _apiClient.Post("/api/v1/Operations/List", new OperationGetRequest());
            Assert.True(getOperationsResp.IsSuccessStatusCode);
            ops =  getOperationsResp.Deserialize<IList<OperationResponse>>();

            Assert.Equal(1, ops.Count);
            Assert.True(ops[0].Id != guidToDelete);           
        }

        [Fact]
        public void GetOperation_ShouldRetrieveMultiplePresentOperationsOnUnconditionalGet()
        {
            var res = _apiClient.Post("/api/v1/Operations/Save", SampleOperation());
            Assert.True(res.IsSuccessStatusCode);

            var secondRes = SampleOperation();
            secondRes.Name = "AnotherOperation";
            Assert.True(_apiClient.Post("/api/v1/Operations/Save", secondRes).IsSuccessStatusCode);

            var getOperationsResp = _apiClient.Post("/api/v1/Operations/List", new OperationGetRequest());
            Assert.True(getOperationsResp.IsSuccessStatusCode);
            var ops = getOperationsResp.Deserialize<IList<OperationResponse>>();
            Assert.Equal(2, ops.Count);
        }

        [Fact]
        public void GetOperation_ShouldFilterByName()
        {
            Assert.True(_apiClient.Post("/api/v1/Operations/Save", SampleOperation()).IsSuccessStatusCode);
            Assert.True(_apiClient.Post("/api/v1/Operations/Save", SampleOperation()).IsSuccessStatusCode);

            var otherRes = SampleOperation();
            otherRes.Name = "AnotherOperation";
            Assert.True(_apiClient.Post("/api/v1/Operations/Save", otherRes).IsSuccessStatusCode);

            var getOperationsResp = _apiClient.Post("/api/v1/Operations/List", new OperationGetRequest { Name = "TestOperation" });
            Assert.True(getOperationsResp.IsSuccessStatusCode);
            var ops = getOperationsResp.Deserialize<IList<OperationResponse>>();
            Assert.Equal(2, ops.Count);
        }

        [Fact]
        public void GetOperation_ShouldFilterByWhetherItIsIncome()
        {
            var inputOperation = SampleOperation();
            inputOperation.IsIncome = true;
            Assert.True((_apiClient.Post("/api/v1/Operations/Save", inputOperation)).IsSuccessStatusCode);
            Assert.True((_apiClient.Post("/api/v1/Operations/Save", inputOperation)).IsSuccessStatusCode);

            inputOperation.IsIncome = false;
            Assert.True(_apiClient.Post("/api/v1/Operations/Save", inputOperation).IsSuccessStatusCode);

            var getOperationsResp = _apiClient.Post("/api/v1/Operations/List", new OperationGetRequest { IsIncome = true });
            Assert.True(getOperationsResp.IsSuccessStatusCode);
            var ops = getOperationsResp.Deserialize<IList<OperationResponse>>();
            Assert.Equal(2, ops.Count);

            getOperationsResp = _apiClient.Post("/api/v1/Operations/List", new OperationGetRequest { IsIncome = false });
            Assert.True(getOperationsResp.IsSuccessStatusCode);
            ops = getOperationsResp.Deserialize<IList<OperationResponse>>();
            Assert.Equal(1, ops.Count);
        }

        [Fact]
        public void GetOperation_ShouldFilterByValueInequality()
        {
            Assert.True(_apiClient.Post("/api/v1/Operations/Save", SampleOperation()).IsSuccessStatusCode);
            Assert.True(_apiClient.Post("/api/v1/Operations/Save", SampleOperation()).IsSuccessStatusCode);

            var otherRes = SampleOperation();
            otherRes.Value = 42;
            Assert.True(_apiClient.Post("/api/v1/Operations/Save", otherRes).IsSuccessStatusCode);

            var getOperationsResp = _apiClient.Post("/api/v1/Operations/List", new OperationGetRequest { MinValue = 9000 });
            Assert.True(getOperationsResp.IsSuccessStatusCode);
            var ops = getOperationsResp.Deserialize<IList<OperationResponse>>();
            Assert.Equal(2, ops.Count);

            getOperationsResp = _apiClient.Post("/api/v1/Operations/List", new OperationGetRequest { MinValue = 10000 });
            Assert.True(getOperationsResp.IsSuccessStatusCode);
            ops = getOperationsResp.Deserialize<IList<OperationResponse>>();
            Assert.Equal(0, ops.Count);

            getOperationsResp = _apiClient.Post("/api/v1/Operations/List", new OperationGetRequest { MaxValue = 50 });
            Assert.True(getOperationsResp.IsSuccessStatusCode);
            ops = getOperationsResp.Deserialize<IList<OperationResponse>>();
            Assert.Equal(1, ops.Count);

            getOperationsResp = _apiClient.Post("/api/v1/Operations/List", new OperationGetRequest { MaxValue = 10 });
            Assert.True(getOperationsResp.IsSuccessStatusCode);
            ops = getOperationsResp.Deserialize<IList<OperationResponse>>();
            Assert.Equal(0, ops.Count);
        }

        private OperationSaveRequest SampleOperation() => new OperationSaveRequest()
        {
            Name = "TestOperation",
            Value = 9000.01M,
            IsIncome = false,
            OccuredAt = DateTime.Now,
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