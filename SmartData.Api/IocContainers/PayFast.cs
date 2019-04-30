using Microsoft.Extensions.DependencyInjection;

using SmartData.Payfast;
using SmartData.Payfast.Mappers;

namespace SmartData.Api.IocContainers
{
    public class PayFast
    {
        public static void Initialise(IServiceCollection services)
        {
            services.AddSingleton<IPayFastService, PayFastService>();
            services.AddSingleton<PayFastRequestMapper>();
        }
    }
}
