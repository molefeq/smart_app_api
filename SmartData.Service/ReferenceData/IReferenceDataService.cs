using SmartData.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartData.Service.ReferenceData
{
    public interface IReferenceDataService
    {
        StaticDataModel GetStaticData();
        List<ReferenceDataModel> GetCountries();
        Task BulkInsertCountries();
        Task BulkInsertExchangeRates(string baseCurrency);
    }
}
