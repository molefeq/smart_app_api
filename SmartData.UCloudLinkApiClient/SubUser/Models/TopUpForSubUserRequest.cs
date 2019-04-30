using Newtonsoft.Json;

namespace SmartData.UCloudLinkApiClient.SubUser.Models
{
    public class TopUpForSubUserRequest
    {
        [JsonProperty(PropertyName = "streamNo")]
        public string StreamNo { get; set; }

        [JsonProperty(PropertyName = "partnerCode")]
        public string PartnerCode { get; set; } 

        [JsonProperty(PropertyName = "loginCustomerId")]
        public string LoginCustomerId { get; set; }

        [JsonProperty(PropertyName = "langType")]
        public string LangType { get; set; }

        [JsonProperty(PropertyName = "operateType")]
        public string OperateType { get; set; }

        [JsonProperty(PropertyName = "userCode")]
        public string UserCode { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }
    }
}
