using Newtonsoft.Json;

namespace SmartData.UCloudLinkApiClient.BusinessPartner.Models
{
    public class LoginResponse
    {
        [JsonProperty(PropertyName = "streamNo")]
        public string StreamNo { get; set; }

        [JsonProperty(PropertyName = "resultCode")]
        public string ResultCode { get; set; }

        [JsonProperty(PropertyName = "resultDesc")]
        public string ResultDescription { get; set; }

        [JsonProperty(PropertyName = "data")]
        public LoginResponseData LoginResponseData { get; set; }
    }
}
