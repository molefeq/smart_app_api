using SmartData.ReferenceApi.Models;
using System.Threading.Tasks;

namespace SmartData.ReferenceApi.HereApi
{
    public interface IReverseGeocodeService
    {
        Task<GeocodeAddress> ReverseGeocode(string latitude, string longitude);
    }
}
