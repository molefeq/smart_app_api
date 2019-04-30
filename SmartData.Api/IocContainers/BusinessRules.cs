using Microsoft.Extensions.DependencyInjection;
using SmartData.Service.Account;

namespace SmartData.Api.IocContainers
{
    public class BusinessRules
    {
        public static void Initialise(IServiceCollection services)
        {
            services.AddSingleton<AccountBusinessRules>();
        }
    }
}
