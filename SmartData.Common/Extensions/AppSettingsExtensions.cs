using Microsoft.Extensions.Configuration;
using SqsLibraries.Common.Email.Models;
using SqsLibraries.Common.Extensions;

namespace SmartData.Common.Extensions
{
    public static class AppSettingsExtensions
    {
        #region UCloudLink Information

        public static string UCloudLinkBaseUrl(this IConfiguration configuration)
        {
            return configuration["ucloudlink:baseUrl"];
        }

        #region UCloudLink Business Partner Information

        public static string UCloudLinkBusinessPartnerLoginUrl(this IConfiguration configuration)
        {
            return configuration["ucloudlink:businessPartner:loginUrl"];
        }

        public static string UCloudLinkBusinessPartnerPassword(this IConfiguration configuration)
        {
            return configuration["ucloudlink:businessPartner:password"];
        }

        public static string UCloudLinkBusinessPartnerClientId(this IConfiguration configuration)
        {
            return configuration["ucloudlink:businessPartner:clientId"];
        }

        public static string UCloudLinkBusinessPartneClientSecret(this IConfiguration configuration)
        {
            return configuration["ucloudlink:businessPartner:clientSecret"];
        }

        public static string UCloudLinkBusinessPartnerUserCode(this IConfiguration configuration)
        {
            return configuration["ucloudlink:businessPartner:userCode"];
        }

        public static string UCloudLinkBusinessPartnerLangType(this IConfiguration configuration)
        {
            return configuration["ucloudlink:businessPartner:langType"];
        }

        public static string UCloudLinkBusinessPartnerPartnerCode(this IConfiguration configuration)
        {
            return configuration["ucloudlink:businessPartner:partnerCode"];
        }

        public static string UCloudLinkBusinessPartnerMvnoCode(this IConfiguration configuration)
        {
            return configuration["ucloudlink:businessPartner:mvnoCode"];
        }

        public static string UCloudLinkBusinessPartnerStreamNo(this IConfiguration configuration)
        {
            return configuration["ucloudlink:businessPartner:streamNo"];
        }

        #endregion

        #endregion

        #region Email Settings

        public static EmailConfiguration EmailConfiguration(this IConfiguration configuration)
        {
            return new EmailConfiguration
            {
                SmtpServer = configuration["email:configuration:smtpserver"],
                SmtpPortNumber = configuration["email:configuration:smtpport"].ToInteger(),
                Username = configuration["email:configuration:username"],
                Password = configuration["email:configuration:password"]
            };
        }

        public static EmailAddress InfoAddress(this IConfiguration configuration)
        {
            return new EmailAddress
            {
                Address = configuration["email:infofrom:address"],
                Name = configuration["email:infofrom:name"]
            };
        }

        public static EmailAddress ErrorAddress(this IConfiguration configuration)
        {
            return new EmailAddress
            {
                Address = configuration["email:error:address"],
                Name = configuration["email:error:name"]
            };
        }

        #endregion

        public static string SiteUrl(this IConfiguration configuration)
        {
            return configuration["site:url"];
        }

        public static string SiteName(this IConfiguration configuration)
        {
            return configuration["site:name"];
        }

        public static string PasswordResetUrl(this IConfiguration configuration)
        {
            return configuration["site:passwordreseturl"];
        }

        public static string ApplicationName(this IConfiguration configuration)
        {
            return configuration["applicationname"];
        }

        public static string RestCountriesUrl(this IConfiguration configuration)
        {
            return configuration["restcountries:url"];
        }

        public static string OpenRatesUrl(this IConfiguration configuration)
        {
            return configuration["openrates:url"];
        }

        #region "here API"
        public static string HereApiAppId(this IConfiguration configuration)
        {
            return configuration["hereApi:appId"];
        }
        public static string HereApiAppCode(this IConfiguration configuration)
        {
            return configuration["hereApi:appCode"];
        }
        public static string HereApiAppReverseGeocodeUrl(this IConfiguration configuration)
        {
            return configuration["hereApi:reverseGeocodeUrl"];
        }
        #endregion
    }
}