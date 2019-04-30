using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using SmartData.Common.Extensions;
using SmartData.Data.ViewModels;
using SmartData.UCloudLinkApiClient.Constants;
using SmartData.UCloudLinkApiClient.SubUser.Models;

namespace SmartData.Service.RequestMappers
{
    public class TopUpForSubUserRequestMapper
    {
        private IMemoryCache cache;
        private IConfiguration configuration;
        public TopUpForSubUserRequest MapToRequest(BuyDataModel buyDataModel)
        {
            return new TopUpForSubUserRequest()
            {
                StreamNo = configuration.UCloudLinkBusinessPartnerStreamNo(),
                PartnerCode = configuration.UCloudLinkBusinessPartnerPartnerCode(),
                LoginCustomerId = cache.LoginCustomerId(),
                LangType = UCloudLinkConstants.DEFAULT_LANGUAGE,
                UserCode = buyDataModel.EmailAddress,
                OperateType = "1",
                Amount = buyDataModel.Amount
            };
        }
    }
}
