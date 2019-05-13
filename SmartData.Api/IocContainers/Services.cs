using Microsoft.Extensions.DependencyInjection;
using SmartData.Service.Account;
using SmartData.Service.Device;
using SmartData.Service.Payment;
using SmartData.Service.ReferenceData;
using SmartData.Service.Topup;
using SqsLibraries.Common.Email;

namespace SmartData.Api.IocContainers
{
    public class Services
    {
        public static void Initialise(IServiceCollection services)
        {
            services.AddScoped<IEmailHandler, SmtpMailGunEmailHandler>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IReferenceDataService, ReferenceDataService>();
            services.AddScoped<IPaymentService, PayFastPaymentService>();
            services.AddScoped<ITopupService, TopupService>();
        }
    }
}
