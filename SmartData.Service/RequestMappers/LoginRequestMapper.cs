using Microsoft.Extensions.Configuration;
using SmartData.Common.Extensions;
using SmartData.UCloudLinkApiClient.BusinessPartner.Models;

namespace SmartData.Service.RequestMappers
{
    public class LoginRequestMapper
    {
        public LoginRequest MapToRequest(IConfiguration confugaration)
        {
            return new LoginRequest()
            {
                StreamNo = confugaration.UCloudLinkBusinessPartnerStreamNo(),
                ClientId = confugaration.UCloudLinkBusinessPartnerClientId(),
                ClientSecret = confugaration.UCloudLinkBusinessPartneClientSecret(),
                UserCode = confugaration.UCloudLinkBusinessPartnerUserCode(),
                LangType = confugaration.UCloudLinkBusinessPartnerLangType(),
                Password = confugaration.UCloudLinkBusinessPartnerPassword(),
                PartnerCode = confugaration.UCloudLinkBusinessPartnerPartnerCode(),
                MvnoCode = confugaration.UCloudLinkBusinessPartnerMvnoCode()
            };
        }
    }
}
