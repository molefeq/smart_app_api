using System;

namespace SmartData.UCloudLinkApiClient.Exceptions
{
    public class UCloudlinkValidationException : Exception
    {
        public string HttpResponseText { get; set; }

        public UCloudlinkValidationException() : base()
        {
        }

        public UCloudlinkValidationException(string message, string httpResponseText) : base(message)
        {
            HttpResponseText = httpResponseText;
        }
    }
}
