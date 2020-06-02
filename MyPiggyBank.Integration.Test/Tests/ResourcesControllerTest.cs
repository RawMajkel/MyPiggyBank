using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyPiggyBank.Core.Protocol;
using MyPiggyBank.Core.Service;
using MyPiggyBank.Data;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Repository;
using MyPiggyBank.Integration.Test.Responses;
using Xunit;

namespace MyPiggyBank.Integration.Test.Tests {
    public class ResourcesControllerTest {
        private readonly RestApiClient _apiClient;

        public ResourcesControllerTest() {
            _apiClient = new RestApiClient();
        }

        [Fact]
        public async void CreateResource_CorrectData_ShouldAddToDb() {
            var inputResource = new Resource() {
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

            var resources = await _apiClient.GetAsync<PagedList<Resource>>("/api/v1/Resources?Name=TestResource");
            Assert.True(resources.Count == 1);
            Assert.True(resources[0].Id != Guid.Empty);
            Assert.True(resources[0].Value > 9000);
        }
    }
}