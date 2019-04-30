using Newtonsoft.Json;

namespace SmartData.UCloudLinkApiClient.SubUser.Models
{
    public class SubUserRequest
    {
        [JsonProperty(PropertyName = "userCode")]
        public string UserCode { get; set; }

        [JsonProperty(PropertyName = "userPwd")]
        public string UserPassword { get; set; }

        [JsonProperty(PropertyName = "imei")]
        public string Imei { get; set; }

        [JsonProperty(PropertyName = "tmlPwd")]
        public string TerminallPassword { get; set; }

        [JsonProperty(PropertyName = "registerType")]
        public string RegisterType { get; set; } = "EMAIL";

        [JsonProperty(PropertyName = "countryCode")]
        public string CountryCode { get; set; }
    }
}
