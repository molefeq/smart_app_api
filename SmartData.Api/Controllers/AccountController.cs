using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SmartData.Api.ActionResultHelpers;
using SmartData.Api.Extensions;
using SmartData.Api.Providers;
using SmartData.Data.ViewModels;
using SmartData.Service.Account;

using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartData.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AccountController : BaseController
    {
        private LoginProvider loginProvider;
        private IAccountService accountService;

        public AccountController(LoginProvider loginProvider, IAccountService accountService)
        {
            this.loginProvider = loginProvider;
            this.accountService = accountService;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserModel), 200)]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return new ValidationActionResult(ModelState);
            }

            var userModel = await loginProvider.Login(model);

            return Ok(userModel);
        }

        [HttpPost("{username}")]
        [AllowAnonymous]
        public IActionResult ResetPasswordRequest([FromRoute] string username)
        {
            accountService.PasswordResetRequest(username);

            return Ok();
        }


        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AccountModel), 200)]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return new ValidationActionResult(ModelState);
            }

            var accountModel = await accountService.Register(model);

            return Ok(accountModel);
        }

        [AllowAnonymous]
        [HttpGet("{provider}")]
        public async Task ExternalLogin([FromRoute]string provider, string error = null)
        {
            await HttpContext.ChallengeAsync("Google", new AuthenticationProperties() { RedirectUri = "/signin-google" });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/signin-google")]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            return Redirect("/api/account/MyClaims");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> MyClaims(string returnUrl = null, string remoteError = null)
        {
            var user = User.Identity as ClaimsIdentity;
            return Ok("/account/MyClaims");
        }

    }
}