using System.Collections.Generic;

namespace SmartData.ReferenceApi.Models
{
    using Newtonsoft.Json;
    public class Country
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "alpha2Code")]
        public string Alpha2Code { get; set; }

        [JsonProperty(PropertyName = "alpha3Code")]
        public string Alpha3Code { get; set; }

        [JsonProperty(PropertyName = "currencies")]
        public List<Currency> Currencies { get; set; }
    }
}
