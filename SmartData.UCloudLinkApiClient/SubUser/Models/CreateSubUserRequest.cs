using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartData.UCloudLinkApiClient.SubUser.Models
{
    public class CreateSubUserRequest
    {
        public CreateSubUserRequest()
        {
            ChildUserVoList = new List<SubUserRequest>();
        }

        [JsonProperty(PropertyName = "streamNo")]
        public string StreamNo { get; set; }

        [JsonProperty(PropertyName = "partnerCode")]
        public string PartnerCode { get; set; }

        [JsonProperty(PropertyName = "loginCustomerId")]
        public string LoginCustomerId { get; set; }

        [JsonProperty(PropertyName = "langType")]
        public string LangType { get; set; }

        [JsonProperty(PropertyName = "childUserVoList")]
        public List<SubUserRequest> ChildUserVoList { get; set; }
    }
}
