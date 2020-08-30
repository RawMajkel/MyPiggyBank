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
        }

        [Fact]
        public void CreateOperation_ShouldSuccessfullyPost()
        {

        }

        [Fact]
        public void CreateOperation_ShouldSuccessfullyPostMultipleTimes()
        {

        }

        [Fact]
        public void CreateOperation_ShouldPostCorrectData()
        {

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