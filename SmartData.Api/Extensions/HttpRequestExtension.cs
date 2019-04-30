using Microsoft.AspNetCore.Http;

namespace SmartData.Api.Extensions
{
    public static class HttpRequestExtension
    {
        public static string CurrentUrl(this HttpRequest request)
        {
            return request.Scheme + "://" + request.Host.Value;
        }
    }
}
