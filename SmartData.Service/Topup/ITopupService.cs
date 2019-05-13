using SmartData.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartData.Service.Topup
{
    public interface ITopupService
    {
        Task<List<TopupModel>> GetTopupOptions(GeoLocation geoLocation, long userId);
    }
}
