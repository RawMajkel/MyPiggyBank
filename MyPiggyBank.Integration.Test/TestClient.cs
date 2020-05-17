using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace MyPiggyBank.Integration.Test
{
    public class TestClient
    {
        private static string _localhost = "http://localhost:5001";
        private static TestServer _server;

        public static HttpClient CreateHost()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json")
                .Build();

            var webHostBuilder = new WebHostBuilder()
                .UseConfiguration(configurationBuilder)
                .UseStartup<StartupTest>()
                .UseKestrel()
                .UseUrls(_localhost);
                 
            _server = new TestServer(webHostBuilder);

            var client = _server.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
