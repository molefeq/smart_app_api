using SmartData.Data.ViewModels;
using SmartData.UCloudLinkApiClient.SubUser.Models;
using SqsLibraries.Common.Utilities;
using System;
using AM = SmartData.DataAccess.Models;

namespace SmartData.Service.DataMappers
{
    public class AccountMapper
    {
        public UserModel MapToUserModel(AM.Account account, SubUserResponse subUserResponse)
        {
            UserModel userModel = new UserModel
            {
                Id = account.Id,
                UserName = account.Username,
                RoleId = account.RoleId,
                RoleName = account.Role.Name,
                Firstname = account.Firstname,
                Lastname = account.LastName
            };

            return userModel;
        }

        public AccountModel MapToAccountModel(AM.Account account)
        {
            AccountModel accountModel = new AccountModel();

            return accountModel;
        }

        public AM.Account MapFromRegisterModel(RegisterModel registerModel)
        {
            AM.Account account = new AM.Account();

            account.Username = registerModel.EmailAddress;
            account.Firstname = registerModel.FirstName;
            account.LastName = registerModel.LastName;
            account.EmailAddress = registerModel.EmailAddress;
            account.CountryId = registerModel.CountryId;
            account.CreateDate = DateTime.Now;
            account.IsFirstTimeLogin = true;
            account.PasswordSalt = GeneratePassword.PasswordSalt();
            account.Password = GeneratePassword.HashedPassword(registerModel.Password, account.PasswordSalt);

            return account;
        }
    }
}
