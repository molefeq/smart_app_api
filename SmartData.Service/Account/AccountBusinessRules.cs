using SmartData.Common.Exceptions;
using SmartData.Data.ViewModels;
using SmartData.DataAccess.Repositories;
using SmartData.UCloudLinkApiClient.SubUser.Models;
using SqsLibraries.Common.Utilities;
using SqsLibraries.Common.Utilities.ResponseObjects;
using System.Collections.Generic;
using System.Linq;
using AM = SmartData.DataAccess.Models;

namespace SmartData.Service.Account
{
    public class AccountBusinessRules
    {
        public void LoginCheck(LoginModel loginModel, GenericRepository<AM.Account> accountRepository, List<SubUserResponse> users)
        {
            if (string.IsNullOrEmpty(loginModel.Username) || string.IsNullOrEmpty(loginModel.Password))
            {
                throw new ResponseValidationException(ResponseMessage.ToError("username and password is required."));
            }

            var account = accountRepository.GetById(a => a.Username == loginModel.Username);

            if (account == null)
            {
                throw new ResponseValidationException(ResponseMessage.ToError("username or Password is incorrect."));
            }

            byte[] hashedPassword = GeneratePassword.HashedPassword(loginModel.Password, account.PasswordSalt);

            account = accountRepository.GetById(a => a.Username == loginModel.Username && a.Password == hashedPassword);

            if (account == null)
            {
                throw new ResponseValidationException(ResponseMessage.ToError("username or Password is incorrect."));
            }

            var user = users.Where(a => loginModel.Username.Equals(a.UserCode)).FirstOrDefault();

            if (user == null)
            {
                throw new ResponseValidationException(ResponseMessage.ToError("username or Password is incorrect."));
            }
        }

        public void ResetPasswordRequestCheck(string username, GenericRepository<AM.Account> accountRepository)
        {
            var account = accountRepository.GetById(a => a.Username == username);

            if (account == null)
            {
                throw new ResponseValidationException(ResponseMessage.ToError("password reset failed, user does not exists."));
            }
        }

        public void RegisterCheck(RegisterModel registerModel, GenericRepository<AM.Account> accountRepository)
        {
            if (registerModel == null)
            {
                throw new ResponseValidationException(ResponseMessage.ToError("The account entry you trying to create does not exist."));
            }

            if (accountRepository.GetById(a => a.EmailAddress.Equals(registerModel.EmailAddress)) != null)
            {
                throw new ResponseValidationException(ResponseMessage.ToError("emailAddress", "Email address is currently being used another user."));
            }

        }
    }
}
