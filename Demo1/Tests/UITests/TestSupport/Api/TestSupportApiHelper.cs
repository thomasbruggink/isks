using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using InfoSupport.Tessler.Core;
using Newtonsoft.Json;

namespace UITests.TestSupport.Api
{
    public class TestSupportApiHelper
    {
        private readonly Uri _baseUri;

        public TestSupportApiHelper()
        {
            _baseUri = new Uri("http://localhost:58011/");
        }

        public dynamic Get(string endpoint)
        {
            var webClient = new HttpClient
            {
                BaseAddress = _baseUri
            };

            var responseMessage = webClient.GetAsync(endpoint).Result;

            return new ApiResponse
            {
                Status = responseMessage.StatusCode,
                Content = JsonConvert.DeserializeObject<dynamic>(responseMessage.Content.ReadAsStringAsync().Result)
            };
        }

        public dynamic Post(string endpoint, dynamic content)
        { 
            var webClient = new HttpClient
            {
                BaseAddress = _baseUri
            };

            var responseMessage = webClient.PostAsync(endpoint, new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json")).Result;

            return new ApiResponse
            {
                Status = responseMessage.StatusCode,
                Content = JsonConvert.DeserializeObject<dynamic>(responseMessage.Content.ReadAsStringAsync().Result)
            };
        }
    }
}
