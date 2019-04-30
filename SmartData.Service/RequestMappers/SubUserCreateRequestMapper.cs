using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using SmartData.Common.Extensions;
using SmartData.Data.ViewModels;
using SmartData.UCloudLinkApiClient.Constants;
using SmartData.UCloudLinkApiClient.SubUser.Models;
using System.Collections.Generic;

namespace SmartData.Service.RequestMappers
{
    public class CreateSubUserRequestMapper
    {
        private IMemoryCache cache;
        private IConfiguration configuration;

        public CreateSubUserRequestMapper(IMemoryCache cache, IConfiguration configuration)
        {
            this.cache = cache;
            this.configuration = configuration;
        }

        public CreateSubUserRequest MapToRequest(RegisterModel registerModel)
        {
            return new CreateSubUserRequest()
            {
                StreamNo = configuration.UCloudLinkBusinessPartnerStreamNo(),
                PartnerCode = configuration.UCloudLinkBusinessPartnerPartnerCode(),
                LoginCustomerId = cache.LoginCustomerId(),
                LangType = UCloudLinkConstants.DEFAULT_LANGUAGE,
                ChildUserVoList=new List<SubUserRequest>
                {
                    new SubUserRequest
                    {
                        UserCode = registerModel.EmailAddress,
                        UserPassword = registerModel.Password
                    }
                }
            };
        }
    }
}
