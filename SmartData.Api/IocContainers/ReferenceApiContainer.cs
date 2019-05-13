using Microsoft.Extensions.DependencyInjection;
using SmartData.ReferenceApi.HereApi;
using SmartData.ReferenceApi.Mappers;
using SmartData.ReferenceApi.OpenRatesApi;
using SmartData.ReferenceApi.RestCountriesApi;

namespace SmartData.Api.IocContainers
{
    public class ReferenceApiContainer
    {
        public static void Initialise(IServiceCollection services)
        {
            services.AddSingleton<ICountryRestService, CountryRestService>();
            services.AddSingleton<RateMapper>();
            services.AddSingleton<IExchangeRateService, ExchangeRateService>();
            services.AddSingleton<IReverseGeocodeService, ReverseGeocodeService>();
        }
    }
}
