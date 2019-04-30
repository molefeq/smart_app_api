using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using SmartData.Common.Extensions;
using SmartData.Data.ViewModels.Device;
using SmartData.UCloudLinkApiClient.Constants;
using SmartData.UCloudLinkApiClient.SubUser.Models;

namespace SmartData.Service.RequestMappers
{
    public class BindSubUserToDeviceRequestMapper
    {
        private IMemoryCache cache;
        private IConfiguration configuration;

        public BindSubUserToDeviceRequestMapper(IMemoryCache cache, IConfiguration configuration)
        {
            this.cache = cache;
            this.configuration = configuration;
        }

        public BindSubUserToDeviceRequest MapToRequest(LinkDeviceModel linkDeviceModel)
        {
            return new BindSubUserToDeviceRequest()
            {
                StreamNo = configuration.UCloudLinkBusinessPartnerStreamNo(),
                PartnerCode = configuration.UCloudLinkBusinessPartnerPartnerCode(),
                LoginCustomerId = cache.LoginCustomerId(),
                LangType = UCloudLinkConstants.DEFAULT_LANGUAGE,
                UserCode = linkDeviceModel.EmailAddress,
                Imei = linkDeviceModel.SerialNumber,
                TerminalPassword = linkDeviceModel.TerminalPassword,
                ForceFlag = true
            };
        }
    }
}
