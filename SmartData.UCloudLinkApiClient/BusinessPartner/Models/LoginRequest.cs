using Newtonsoft.Json;

namespace SmartData.UCloudLinkApiClient.BusinessPartner.Models
{
    public class LoginRequest
    {
        [JsonProperty(PropertyName = "streamNo")]
        public string StreamNo { get; set; } = "TianPai2018071100000000004";//"WEB2019032211553134923";//"UKGRP20151113161019000001";

        [JsonProperty(PropertyName = "clientId")]
        public string ClientId { get; set; } = "5b0be0acda811e0f013d5753";// "5b9a17711ffdba3439b05709";// "bb18c96ca5fd4c82bca5788f9a0a4311"; //

        [JsonProperty(PropertyName = "clientSecret")]
        public string ClientSecret { get; set; } = "5b0be0acda811e0f013d5754";//"234sdfert345345tgrey45345"; // "563b2c3404d0321a149a0ade25b54c82"; //

        [JsonProperty(PropertyName = "userCode")]
        public string UserCode { get; set; } = "TianPai_APITEST"; //"Demo_Admin";// "8618520846179";

        [JsonProperty(PropertyName = "langType")]
        public string LangType { get; set; } = "zh-CN"; //"en-US";

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; } = "72663380f70509957c7ce471b91cb93a"; //"448f98ce57961fc0d738d6abcefca612";//;"Demo_Admin@3jNA"; //"448f98ce57961fc0d738d6abcefca612";//"561b7301dc199e1fec5744ba5431f8d7"; //

        [JsonProperty(PropertyName = "partnerCode")]
        public string PartnerCode { get; set; } = "TianPai_APITEST"; //"IOTSU";// "UKGRP"; //

        [JsonProperty(PropertyName = "mvnoCode")]
        public string MvnoCode { get; set; } = "IOTSU";// "MVNO";
    }
}
