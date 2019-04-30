using Newtonsoft.Json;

namespace SmartData.UCloudLinkApiClient.BusinessPartner.Models
{
    public class LoginResponseData
    {
        [JsonProperty(PropertyName = "accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }
    }
}
