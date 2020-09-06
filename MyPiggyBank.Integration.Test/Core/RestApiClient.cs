using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using MyPiggyBank.Core.Protocol.Account.DTO;
using MyPiggyBank.Core.Protocol.Account.Requests;
using MyPiggyBank.Data;
using Newtonsoft.Json;

namespace MyPiggyBank.Integration.Test
{
    public class RestApiClient
    {
        public MyPiggyBankContext PiggyBankContext { get; }

        public RestApiClient()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json")
                .Build();

            var webHostBuilder = new WebHostBuilder()
                .UseEnvironment("Test")
                .UseConfiguration(configurationBuilder)
                .UseStartup<StartupTest>()
                .UseKestrel()
                .UseUrls(_localhost);

            var server = new TestServer(webHostBuilder);
            PiggyBankContext = server.Host.Services.GetService(typeof(MyPiggyBankContext)) as MyPiggyBankContext;

            _client = server.CreateClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonHeader));
        }

        public HttpResponseMessage Get(string url)
            => _client.GetAsync(url).Result;

        public HttpResponseMessage Post<T>(string url, T input)
            => _client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, JsonHeader)).Result;

        public HttpResponseMessage Delete(string url)
            => _client.DeleteAsync(url).Result;

        public bool TestUserAuth()
        {
            Post("/api/v1/Account/Register", new RegisterRequest() {
                Email = "email@gmail.com",
                Password = "Pa$$word1",
                Username = "TestUser"
            });

            var loginResp = Post("/api/v1/Account/Login", new LoginRequest() {
                Email = "email@gmail.com",
                Password = "Pa$$word1"
            });

            var authData = loginResp.Deserialize<AuthorizationToken>();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authData.Token);

            return authData.Token.Length != 0;
        }

        private readonly HttpClient _client;
        private const string _localhost = "http://localhost:5001";
        private const string JsonHeader = "application/json";
    }
}
