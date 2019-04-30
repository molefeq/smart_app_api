using Microsoft.Extensions.Caching.Memory;

namespace SmartData.Common.Extensions
{
    public static class MemoryCacheExtension
    {
        private static string LOGIN_CUSTOMER_ID = "LoginCustomerId";
        private static string ACCESS_TOKEN = "AccessToken";

        public static string LoginCustomerId(this IMemoryCache cache)
        {
            return cache.Get<string>(LOGIN_CUSTOMER_ID);
        }

        public static void SetLoginCustomerId(this IMemoryCache cache, string loginCustomerId)
        {
            cache.Set(LOGIN_CUSTOMER_ID, loginCustomerId);
        }

        public static string AccessToken(this IMemoryCache cache)
        {
            return cache.Get<string>(ACCESS_TOKEN);
        }

        public static void SetAccessToken(this IMemoryCache cache, string accessToken)
        {
            cache.Set(ACCESS_TOKEN, accessToken);
        }
    }
}
