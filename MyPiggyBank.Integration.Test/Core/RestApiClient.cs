using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        private readonly HttpClient _client;
        private const string _localhost = "http://localhost:5001";
        private const string JsonHeader = "application/json";

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

        public MyPiggyBankContext PiggyBankContext { get; }

        public async Task<bool> TestUserAuth()
        {
            var registerResp = await PostAsync("/api/v1/Account/Register", new RegisterRequest() {
                Email = "email@gmail.com",
                Password = "Pa$$word1",
                Username = "TestUser"
            });

            var loginResp = await PostAsync("/api/v1/Account/Login", new LoginRequest() {
                Email = "email@gmail.com",
                Password = "Pa$$word1"
            });

            var token = loginResp.Deserialize<AuthorizationToken>();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);

            return registerResp.IsSuccessStatusCode && loginResp.IsSuccessStatusCode;
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string url, T input)
        {
            var body = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, JsonHeader);
            return await _client.PostAsync(url, body);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url) {
            var resp = await _client.DeleteAsync(url);
            resp.EnsureSuccessStatusCode();
            return resp;
        }


        public async Task<TResponse> GetAsync<TResponse>(string url)
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return response.Deserialize<TResponse>();
        }
    }
}
