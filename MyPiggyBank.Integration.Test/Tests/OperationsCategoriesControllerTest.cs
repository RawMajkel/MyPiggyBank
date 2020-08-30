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

        }

        [Fact]
        public void CreateCyclicOperation_ShouldSuccessfullyPostMultipleTimes()
        {

        }

        [Fact]
        public void CreateCyclicOperation_ShouldPostCorrectData()
        {

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