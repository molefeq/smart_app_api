using Newtonsoft.Json.Linq;
using SmartData.ReferenceApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartData.ReferenceApi.Mappers
{
    public class RateMapper
    {
        public List<Rate> MapToRates(string jsonResponse)
        {
            var ratesjObject = JObject.Parse(jsonResponse);
            IList<JToken> results = ratesjObject["rates"].Children().ToList();
            List<Rate> rates = new List<Rate>();

            foreach (JProperty result in results)
            {
                rates.Add(MapToRate(result));
            }

            return rates;
        }

        public Rate MapToRate(JProperty property)
        {
            return new Rate
            {
                Currency = property.Name,
                ExchangeRate = property.Value.Value<decimal>()
            };
        }
    }
}
