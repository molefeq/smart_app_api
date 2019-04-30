using SmartData.UCloudLinkApiClient.BusinessPartner.Models;

using System.Threading.Tasks;

namespace SmartData.UCloudLinkApiClient.BusinessPartner
{
    public interface IBusinessPartnerClient
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
    }
}
