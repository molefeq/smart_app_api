using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using SmartData.Common.Extensions;
using SmartData.Data.ViewModels;
using SmartData.DataAccess;
using SmartData.Service.DataMappers;
using SmartData.Service.RequestMappers;
using SmartData.UCloudLinkApiClient.SubUser;
using SqsLibraries.Common.Email;
using SqsLibraries.Common.Email.Models;

namespace SmartData.Service.Account
{
    public class AccountService : IAccountService
    {
        private static string DEFAULT_USER_ROLE_CODE = "USR";

        private IUnitOfWork unitOfWork;
        private AccountMapper accountMapper;
        private AccountBusinessRules accountBusinessRules;
        private IEmailHandler emailHandler;
        private IMemoryCache cache;
        private IConfiguration configuration;
        private ISubUserClient subUserClient;
        private CreateSubUserRequestMapper createSubUserRequestMapper;
        private FetchSubUsersForBusinessUserRequestMapper fetchSubUsersForBusinessUserRequestMapper;

        public AccountService(IUnitOfWork unitOfWork,
            AccountMapper accountMapper,
            AccountBusinessRules accountBusinessRules,
            IEmailHandler emailHandler,
            IMemoryCache cache,
            IConfiguration configuration,
            ISubUserClient subUserClient,
            CreateSubUserRequestMapper createSubUserRequestMapper,
            FetchSubUsersForBusinessUserRequestMapper fetchSubUsersForBusinessUserRequestMapper)
        {
            this.unitOfWork = unitOfWork;
            this.accountMapper = accountMapper;
            this.accountBusinessRules = accountBusinessRules;
            this.emailHandler = emailHandler;
            this.cache = cache;
            this.configuration = configuration;
            this.subUserClient = subUserClient;
            this.createSubUserRequestMapper = createSubUserRequestMapper;
            this.fetchSubUsersForBusinessUserRequestMapper = fetchSubUsersForBusinessUserRequestMapper;
        }

        public async Task<UserModel> Login(LoginModel loginModel)
        {
            var subUsers = await subUserClient.FetchSubUsersForBusinessUser(fetchSubUsersForBusinessUserRequestMapper.MapToRequest(loginModel), cache.AccessToken());

            accountBusinessRules.LoginCheck(loginModel, unitOfWork.Account, subUsers);

            var account = unitOfWork.Account.GetById(a => a.Username == loginModel.Username, "Role");
            var subUser = subUsers.Where(a => loginModel.Username.Equals(a.UserCode)).FirstOrDefault();

            return accountMapper.MapToUserModel(account, subUser);
        }

        public async Task<AccountModel> Register(RegisterModel registerModel)
        {
            accountBusinessRules.RegisterCheck(registerModel, unitOfWork.Account);
            await CreateUCloudLinkSubUser(registerModel);

            return CreateAccount(registerModel);
        }

        public AccountModel PasswordResetRequest(string username)
        {
            accountBusinessRules.ResetPasswordRequestCheck(username, unitOfWork.Account);

            var account = unitOfWork.Account.GetById(a => a.Username == username, "Company, Organisation, Role");

            account.PasswordResetKey = Guid.NewGuid();
            unitOfWork.Account.Update(account);
            unitOfWork.Save();

            AccountModel accountModel = accountMapper.MapToAccountModel(account);
            SendResetPasswordEmail(accountModel);

            return accountModel;
        }

        #region Private Methods

        private void SendCreateAccountEmail(AccountModel accountModel)
        {
            string subject = string.Format("{0} Email notification ... Welcome to {0}!", configuration.SiteName());
            StringBuilder sb = new StringBuilder();

            // Add email heading
            sb.Append(string.Format("Dear {0} User.", configuration.ApplicationName()));
            sb.Append("<br />");
            sb.Append("<br />");
            sb.Append(string.Format("This Email confirms that your unique profile has been created with the following credentials."));
            sb.Append("<br />");
            sb.Append("<br />");
            sb.Append(string.Format("Username: {0}", accountModel.Username));
            sb.Append("<br />");
            sb.Append(string.Format("Password: {0}", accountModel.Password));
            sb.Append("<br />");
            sb.Append("<br />");
            sb.Append("Follow the steps to finalize your profile.");
            sb.Append("<br />");
            sb.Append("<br />");
            sb.Append("<ol>");
            sb.Append(string.Format("<li>Log on to {0} by clicking on this link <a href='{1}'>{1}</a></li>", configuration.SiteName(), configuration.SiteUrl()));
            sb.Append("<li>Insert your credentials as provided in this email and follow the change password steps.</li>");
            sb.Append("</ol>");

            EmailContent emailContent = new EmailContent
            {
                Configuration = configuration.EmailConfiguration(),
                From = configuration.InfoAddress(),
                To = new List<EmailAddress> { new EmailAddress { Address = accountModel.EmailAddress, Name = accountModel.Firstname + " " + accountModel.LastName } },
                Subject = subject,
                HtmlBody = sb.ToString()
            };

            this.emailHandler.SendEmail(emailContent);
        }

        private void SendResetPasswordEmail(AccountModel accountModel)
        {
            string subject = string.Format("{0} Email notification ... Password Reset to {1}!", configuration.ApplicationName(), configuration.SiteName());
            string siteLink = configuration.PasswordResetUrl() + "?key=" + accountModel.PasswordResetKey.Value.ToString();

            StringBuilder sb = new StringBuilder();

            // Add email heading
            sb.Append(string.Format("Dear {0} User.", configuration.ApplicationName()));
            sb.Append("<br />");
            sb.Append("<br />");
            sb.Append(string.Format("This Email confirms that you have requested a password reset please click the below link to reset your password."));
            sb.Append("<br />");
            sb.Append(string.Format("<a href='{0}'>Click Here</a>", siteLink));
            sb.Append("<br />");

            EmailContent emailContent = new EmailContent
            {
                Configuration = configuration.EmailConfiguration(),
                From = configuration.InfoAddress(),
                To = new List<EmailAddress> { new EmailAddress { Address = accountModel.EmailAddress, Name = accountModel.Firstname + " " + accountModel.LastName } },
                Subject = subject,
                HtmlBody = sb.ToString()
            };

            this.emailHandler.SendEmail(emailContent);
        }

        private async Task CreateUCloudLinkSubUser(RegisterModel registerModel)
        {
            var subUsers = await subUserClient.FetchSubUsersForBusinessUser(fetchSubUsersForBusinessUserRequestMapper.MapToRequest(registerModel), cache.AccessToken());
            var user = subUsers.Where(a => registerModel.EmailAddress.Equals(a.UserCode)).FirstOrDefault();

            if (user == null)
            {
                await subUserClient.CreateSubUser(createSubUserRequestMapper.MapToRequest(registerModel), cache.AccessToken());
            }
        }
        
        private AccountModel CreateAccount(RegisterModel registerModel)
        {
            var role = unitOfWork.Role.GetById(r => DEFAULT_USER_ROLE_CODE.Equals(r.Code));
            var account = accountMapper.MapFromRegisterModel(registerModel);

            // This is a temporary fix untill we decide what to do with role from the the front end.
            account.RoleId = role.Id;

            unitOfWork.Account.Insert(account);
            unitOfWork.Save();

            return accountMapper.MapToAccountModel(unitOfWork.Account.GetById(a => a.Id == account.Id));
        }

        #endregion
    }
}
