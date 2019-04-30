using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using SmartData.Common.Extensions;
using SmartData.Data.ViewModels;
using SmartData.UCloudLinkApiClient.Constants;
using SmartData.UCloudLinkApiClient.SubUser.Models;

namespace SmartData.Service.RequestMappers
{
    public class FetchSubUsersForBusinessUserRequestMapper
    {
        private IMemoryCache cache;
        private IConfiguration configuration;

        public FetchSubUsersForBusinessUserRequestMapper(IMemoryCache cache, IConfiguration configuration)
        {
            this.cache = cache;
            this.configuration = configuration;
        }

        public FetchSubUsersForBusinessUserRequest MapToRequest(string username)
        {
            return new FetchSubUsersForBusinessUserRequest()
            {
                StreamNo = configuration.UCloudLinkBusinessPartnerStreamNo(),
                PartnerCode = configuration.UCloudLinkBusinessPartnerPartnerCode(),
                LoginCustomerId = cache.LoginCustomerId(),
                LangType = UCloudLinkConstants.DEFAULT_LANGUAGE,
                UserCode = username,
                CurrentPage = 1,
                PerPageCount = 100
            };
        }
        
        public FetchSubUsersForBusinessUserRequest MapToRequest(LoginModel loginModel)
        {
            return new FetchSubUsersForBusinessUserRequest()
            {
                StreamNo = configuration.UCloudLinkBusinessPartnerStreamNo(),
                PartnerCode = configuration.UCloudLinkBusinessPartnerPartnerCode(),
                LoginCustomerId = cache.LoginCustomerId(),
                LangType = UCloudLinkConstants.DEFAULT_LANGUAGE,
                UserCode = loginModel.Username,
                CurrentPage = 1,
                PerPageCount = 100
            };
        }

        public FetchSubUsersForBusinessUserRequest MapToRequest(RegisterModel registerModel)
        {
            return new FetchSubUsersForBusinessUserRequest()
            {
                StreamNo = configuration.UCloudLinkBusinessPartnerStreamNo(),
                PartnerCode = configuration.UCloudLinkBusinessPartnerPartnerCode(),
                LoginCustomerId = cache.LoginCustomerId(),
                LangType = UCloudLinkConstants.DEFAULT_LANGUAGE,
                UserCode = registerModel.EmailAddress,
                CurrentPage = 1,
                PerPageCount = 100
            };
        }
    }
}
