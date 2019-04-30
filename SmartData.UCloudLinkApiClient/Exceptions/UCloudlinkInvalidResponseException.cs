using System;

namespace SmartData.UCloudLinkApiClient.Exceptions
{
    public class UCloudlinkInvalidResponseException : Exception
    {
        public string HttpResponseText { get; set; }

        public UCloudlinkInvalidResponseException() : base()
        {
        }

        public UCloudlinkInvalidResponseException(string message, string httpResponseText) : base(message)
        {
            HttpResponseText = httpResponseText;
        }
    }
}
