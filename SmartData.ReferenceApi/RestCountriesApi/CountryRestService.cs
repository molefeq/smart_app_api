using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;

using SmartData.Common.Extensions;
using SmartData.ReferenceApi.Models;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SmartData.ReferenceApi.RestCountriesApi
{
    public class CountryRestService : ICountryRestService
    {
        private IConfiguration configuration;

        public CountryRestService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<List<Country>> GetCountries()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, configuration.RestCountriesUrl());
            var response = await client.SendAsync(httpRequest);
            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Country>>(responseContent);
        }
    }
}
