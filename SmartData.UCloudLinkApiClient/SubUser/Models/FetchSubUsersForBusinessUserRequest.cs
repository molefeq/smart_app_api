using Newtonsoft.Json;

namespace SmartData.UCloudLinkApiClient.SubUser.Models
{
    public class FetchSubUsersForBusinessUserRequest
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

        [JsonProperty(PropertyName = "currentPage")]
        public int CurrentPage { get; set; }

        [JsonProperty(PropertyName = "perPageCount")]
        public int PerPageCount { get; set; }

        [JsonProperty(PropertyName = "deleteFlag")]
        public string DeleteFlag { get; set; } = "1";
    }
}

