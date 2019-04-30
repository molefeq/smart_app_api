using Microsoft.Extensions.DependencyInjection;
using SmartData.UCloudLinkApiClient.BusinessPartner;
using SmartData.UCloudLinkApiClient.SubUser;

namespace SmartData.Api.IocContainers
{
    public class UCloudLinkClients
    {
        public static void Initialise(IServiceCollection services)
        {
            services.AddSingleton<IBusinessPartnerClient, BusinessPartnerClient>();
            services.AddSingleton<ISubUserClient, SubUserClient>();
        }
    }
}
