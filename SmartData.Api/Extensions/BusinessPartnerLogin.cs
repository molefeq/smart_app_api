using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Caching.Memory;
using SmartData.Common.Extensions;
using SmartData.Service.UCloudLink.BusinessPartner;

namespace SmartData.Api.Extensions
{
    public static class BusinessPartnerLogin
    {
        public static IApplicationBuilder UseBusinessPartnerLoginHandler(this IApplicationBuilder app)
        {
            var businessPartnerService = app.ApplicationServices.GetService(typeof(IBusinessPartnerService)) as IBusinessPartnerService;
            var cache = app.ApplicationServices.GetService(typeof(IMemoryCache)) as IMemoryCache;


            return app.Use(async (context, next) =>
            {
                HandleBusinessPartnerLogin(businessPartnerService, cache);
                await next.Invoke();
            });
        }

        public static void HandleBusinessPartnerLogin(IBusinessPartnerService businessPartnerService, IMemoryCache cache)
        {
            if (string.IsNullOrEmpty(cache.AccessToken()) || string.IsNullOrEmpty(cache.LoginCustomerId()))
            {
                var response = businessPartnerService.Login();

                cache.SetLoginCustomerId(response.LoginResponseData.UserId);
                cache.SetAccessToken(response.LoginResponseData.AccessToken);
            }
        }
    }
}
