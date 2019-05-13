using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using SmartData.Common.Extensions;
using SmartData.ReferenceApi.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SmartData.ReferenceApi.HereApi
{
    public class ReverseGeocodeService : IReverseGeocodeService
    {
        private IConfiguration configuration;

        public ReverseGeocodeService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<GeocodeAddress> ReverseGeocode(string latitude, string longitude)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{configuration.HereApiAppReverseGeocodeUrl()}?app_id={configuration.HereApiAppId()}&app_code={configuration.HereApiAppCode()}&prox={latitude},{longitude},1000&mode=retrieveAddresses&maxresults=1&gen=9");
            var response = await client.SendAsync(httpRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            var address = JObject.Parse(responseContent).SelectToken("Response.View[0].Result[0].Location.Address");

           return address.ToObject<GeocodeAddress>();
        }
    }
}
