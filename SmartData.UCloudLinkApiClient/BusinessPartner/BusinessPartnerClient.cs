using Newtonsoft.Json;

using SmartData.UCloudLinkApiClient.BusinessPartner.Models;
using System.Threading.Tasks;

namespace SmartData.UCloudLinkApiClient.BusinessPartner
{
    public class BusinessPartnerClient : IBusinessPartnerClient
    {
        private readonly UCloudLinkClient _uCloudLinkClient;

        public BusinessPartnerClient(UCloudLinkClient uCloudLinkClient)
        {
            _uCloudLinkClient = uCloudLinkClient;
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var response = await _uCloudLinkClient.PostAsync(loginRequest, "noauth/GrpUserLogin");

            return JsonConvert.DeserializeObject<LoginResponse>(response);
        }
    }
}
