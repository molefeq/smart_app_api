using Microsoft.Extensions.Configuration;

using SmartData.Service.RequestMappers;
using SmartData.UCloudLinkApiClient.BusinessPartner;
using SmartData.UCloudLinkApiClient.BusinessPartner.Models;

namespace SmartData.Service.UCloudLink.BusinessPartner
{
    public class BusinessPartnerService : IBusinessPartnerService
    {
        private IConfiguration confugaration;
        private IBusinessPartnerClient businessPartnerClient;
        private LoginRequestMapper loginRequestMapper;

        public BusinessPartnerService(IConfiguration confugaration,
                                      IBusinessPartnerClient businessPartnerClient,
                                      LoginRequestMapper loginRequestMapper)
        {
            this.confugaration = confugaration;
            this.businessPartnerClient = businessPartnerClient;
            this.loginRequestMapper = loginRequestMapper;
        }

        public LoginResponse Login()
        {
            var request = loginRequestMapper.MapToRequest(confugaration);
            var response = businessPartnerClient.Login(request);

            return response.Result;
        }
    }
}
