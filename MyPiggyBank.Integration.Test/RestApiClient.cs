using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
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

        public async Task<HttpResponseMessage> PostAsync<T>(string url, T input)
        {
            var body = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, JsonHeader);
            return await _client.PostAsync(url, body);
        }

        public async Task<TResponse> GetAsync<TResponse>(string url)
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseAsString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(responseAsString);
        }
    }
}
