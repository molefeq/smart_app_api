using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartData.UCloudLinkApiClient.SubUser.Models
{
    public class TopUpForSubUserResponse
    {
        [JsonProperty(PropertyName = "userCode")]
        public string UserCode { get; set; }

        [JsonProperty(PropertyName = "customerId")]
        public string CustomerId { get; set; }

        [JsonProperty(PropertyName = "accountId")]
        public string AccountId { get; set; }

        [JsonProperty(PropertyName = "balance")]
        public double Balance { get; set; }
    }
}
