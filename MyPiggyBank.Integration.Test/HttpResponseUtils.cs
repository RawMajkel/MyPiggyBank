using System.Net.Http;
using Newtonsoft.Json;

namespace MyPiggyBank.Integration.Test
{
    public static class HttpResponseUtils
    {
        public static TResponse Deserialize<TResponse>(this HttpResponseMessage responseMessage)
        {
            var responseAsString = responseMessage.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<TResponse>(responseAsString);
        }
    }
}
