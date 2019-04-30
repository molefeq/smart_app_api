using Newtonsoft.Json;
using System;

namespace SmartData.UCloudLinkApiClient.SubUser.Models
{
    public class BindSubUserToDeviceRequest
    {
        [JsonProperty(PropertyName = "streamNo")]
        public string StreamNo { get; set; }

        [JsonProperty(PropertyName = "partnerCode")]
        public string PartnerCode { get; set; }

        [JsonProperty(PropertyName = "loginCustomerId")]
        public string LoginCustomerId { get; set; }

        [JsonProperty(PropertyName = "langType")]
        public string LangType { get; set; }

        [JsonProperty(PropertyName = "userCode")]
        public string UserCode { get; set; }

        [JsonProperty(PropertyName = "imei")]
        public string Imei { get; set; }

        [JsonProperty(PropertyName = "tmlPwd")]
        public string TerminalPassword { get; set; }

        [JsonProperty(PropertyName = "forceFlag")]
        public Boolean ForceFlag { get; set; }
    }
}
