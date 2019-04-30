using Newtonsoft.Json;

namespace SmartData.UCloudLinkApiClient.SubUser.Models
{
    public class SubUserResponse
    {
        [JsonProperty(PropertyName = "userCode")]
        public string UserCode { get; set; }

        [JsonProperty(PropertyName = "customerId")]
        public string CustomerId { get; set; }

        [JsonProperty(PropertyName = "imei")]
        public string Imei { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "tmlPwd")]
        public string TerminalPassword { get; set; }

        [JsonProperty(PropertyName = "balance")]
        public decimal Balance { get; set; }

        [JsonProperty(PropertyName = "currencyType")]
        public string CurrencyType { get; set; }
    }
}
