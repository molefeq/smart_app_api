using SmartData.Data.ViewModels;
using System.Threading.Tasks;

namespace SmartData.Service.Account
{
    public interface IAccountService
    {
        Task<UserModel> Login(LoginModel loginmodel);
        Task<AccountModel> Register(RegisterModel registerModel);
        AccountModel PasswordResetRequest(string username);
    }
}
