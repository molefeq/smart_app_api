using Microsoft.Extensions.DependencyInjection;
using SmartData.Service.DataMappers;

namespace SmartData.Api.IocContainers
{
    public class DataMappers
    {
        public static void Initialise(IServiceCollection services)
        {
            services.AddSingleton<AccountMapper>();
            services.AddSingleton<DeviceDetailMapper>();
            services.AddSingleton<OnceOffPaymentMapper>();
            services.AddSingleton<PaymentDetailMapper>();
            services.AddSingleton<CountryMapper>();
            services.AddSingleton<ExchangeRateMapper>();
            services.AddSingleton<TopupOptionMapper>();
        }
    }
}
