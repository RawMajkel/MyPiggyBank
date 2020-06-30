using System;
using System.Collections.Generic;
using MyPiggyBank.Core.Protocol.Resource;
using MyPiggyBank.Core.Protocol.Resource.Responses;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Util;
using Xunit;

namespace MyPiggyBank.Integration.Test.Tests
{
    public class ResourcesControllerTest
    {
        private readonly RestApiClient _apiClient;

        public ResourcesControllerTest() {
            _apiClient = new RestApiClient();
        }

        [Fact]
        public async void CreateResource_CorrectData_ShouldAddToDb()
        {
            var inputResource = new Resource()           
            {
                Name = "TestResource",
                Value = 9000.01M,
                Currency = "USD",
                User = new User() {
                    Email = "someemail@domain.tld",
                    Username = "lUser",
                    PasswordHash = "hahahasz"
                }
            };

            var resp = await _apiClient.PostAsync("/api/v1/Resources", inputResource);
            Assert.True(resp.IsSuccessStatusCode);

            var resources = await _apiClient.GetAsync<IList<ResourceResponse>>("/api/v1/Resources?Name=TestResource");
            Assert.True(resources.Count == 1);
            Assert.True(resources[0].Id != Guid.Empty);
            Assert.True(resources[0].Value > 9000);
        }
    }
}