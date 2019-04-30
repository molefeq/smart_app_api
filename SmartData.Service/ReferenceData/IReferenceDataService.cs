using SmartData.Data.ViewModels;
using System.Collections.Generic;

namespace SmartData.Service.ReferenceData
{
    public interface IReferenceDataService
    {
        StaticDataModel GetStaticData();
        List<ReferenceDataModel> GetCountries();
    }
}
