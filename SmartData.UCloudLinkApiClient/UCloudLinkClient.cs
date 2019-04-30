
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartData.Common.Extensions;
using SmartData.UCloudLinkApiClient.Constants;
using SmartData.UCloudLinkApiClient.Exceptions;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

namespace SmartData.UCloudLinkApiClient
{
    public class UCloudLinkClient
    {
        public HttpClient Client { get; private set; }

        public UCloudLinkClient(HttpClient httpClient, IConfiguration confugaration)
        {
            httpClient.BaseAddress = new Uri(confugaration.UCloudLinkBaseUrl());

            Client = httpClient;
        }

        public async Task<string> PostAsync<T>(T request, string uri) where T : class
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")
            };

            var response =  await Client.SendAsync(httpRequest);
            var responseText = await response.Content.ReadAsStringAsync();

            JObject jsonResponse = JObject.Parse(responseText);

            string resultCode = jsonResponse["resultCode"].Value<string>();

            if (!UCloudLinkConstants.SUCCESS_RESULTCODE.Equals(resultCode))
            {
                throw new UCloudlinkInvalidResponseException($"Fetch users for busniess partner failed", responseText);
            }

            return responseText;
        }
    }
}
