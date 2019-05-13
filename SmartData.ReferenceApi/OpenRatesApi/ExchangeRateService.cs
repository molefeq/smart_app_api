using Microsoft.Extensions.Configuration;
using SmartData.Common.Extensions;
using SmartData.ReferenceApi.Mappers;
using SmartData.ReferenceApi.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SmartData.ReferenceApi.OpenRatesApi
{
    public class ExchangeRateService: IExchangeRateService
    {
        private IConfiguration configuration;
        private RateMapper rateMapper;

        public ExchangeRateService(IConfiguration configuration, RateMapper rateMapper)
        {
            this.configuration = configuration;
            this.rateMapper= rateMapper;
        }

        public async Task<List<Rate>> GetRates(string baseCurrency)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{configuration.OpenRatesUrl()}?base={baseCurrency}");
            var response = await client.SendAsync(httpRequest);
            var responseContent = await response.Content.ReadAsStringAsync();

            return rateMapper.MapToRates(responseContent);
        }
    }
}
