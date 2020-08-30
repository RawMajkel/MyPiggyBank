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
            ResourceId = _apiClient.Get("/api/v1/Resources/List").Deserialize<IList<ResourceResponse>>()[0].Id;
            OpCategoryId = _apiClient.Get("/api/v1/OperationCategories/List").Deserialize<IList<OperationCategoriesResponse>>()[0].Id;
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

            var getOperationResp = _apiClient.Get("/api/v1/Operations/List?Name=TestOperation");
            Assert.True(getOperationResp.IsSuccessStatusCode);

            var ops = getOperationResp.Deserialize<IList<OperationResponse>>();
            Assert.Equal(1, ops.Count);
            Assert.NotEqual(Guid.Empty, ops[0].Id);
            Assert.True(ops[0].Value > 9000);
        }

        [Fact]
        public void DeleteOperation_ShouldDeleteFromDB()
        {

        }

        [Fact]
        public void DeleteOperation_ShouldDeleteOnlyOne()
        {
          
        }

        [Fact]
        public void GetOperation_ShouldRetrieveMultiplePresentOperationsOnUnconditionalGet()
        {

        }

        [Fact]
        public void GetOperation_ShouldFilterByName()
        {

        }

        [Fact]
        public void GetOperation_ShouldFilterByWhetherItIsIncome()
        {

        }

        [Fact]
        public void GetOperation_ShouldFilterByValueInequality()
        {

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