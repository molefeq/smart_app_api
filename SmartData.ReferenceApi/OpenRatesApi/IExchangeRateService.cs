using SmartData.ReferenceApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartData.ReferenceApi.OpenRatesApi
{
    public interface IExchangeRateService
    {
        Task<List<Rate>> GetRates(string baseCurrency);
    }
}
