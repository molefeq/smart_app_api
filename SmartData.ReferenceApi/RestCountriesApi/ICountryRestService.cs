using SmartData.ReferenceApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartData.ReferenceApi.RestCountriesApi
{
    public interface ICountryRestService
    {
        Task<List<Country>> GetCountries();
    }
}
