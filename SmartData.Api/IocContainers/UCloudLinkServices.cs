using Microsoft.Extensions.DependencyInjection;
using SmartData.Service.RequestMappers;
using SmartData.Service.UCloudLink.BusinessPartner;

namespace SmartData.Api.IocContainers
{
    public class UCloudLinkServices
    {
        public static void Initialise(IServiceCollection services)
        {
            services.AddSingleton<LoginRequestMapper>();
            services.AddSingleton<CreateSubUserRequestMapper>();
            services.AddSingleton<FetchSubUsersForBusinessUserRequestMapper>();
            services.AddSingleton<TopUpForSubUserRequestMapper>();
            services.AddSingleton<BindSubUserToDeviceRequestMapper>();
            services.AddSingleton<IBusinessPartnerService, BusinessPartnerService>();
        }
    }
}
