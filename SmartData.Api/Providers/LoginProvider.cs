using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartData.Data.ViewModels;
using SmartData.Service.Account;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartData.Api.Providers
{
    public class LoginProvider
    {
        private IConfiguration configuration;
        private IAccountService accountService;

        public LoginProvider(IConfiguration configuration, IAccountService accountService)
        {
            this.configuration = configuration;
            this.accountService = accountService;
        }

        public async Task<UserModel> Login(LoginModel loginmodel)
        {
            UserModel user = await accountService.Login(loginmodel);

            user.Token = new JwtSecurityTokenHandler().WriteToken(CreateToken(user));

            return user;
        }

        private JwtSecurityToken CreateToken(UserModel userModel)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["token:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = GetClaims(userModel);

            var token = new JwtSecurityToken(
                 issuer: configuration["token:issuer"],
                 audience: configuration["token:audience"],
                 claims: claims,
                 expires: DateTime.Now.AddMinutes(30),
                 signingCredentials: creds);

            return token;
        }

        private List<Claim> GetClaims(UserModel userModel)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userModel.Id.ToString()),
                new Claim("Username", userModel.UserName),
                new Claim("Firstname", userModel.Firstname),
                new Claim("Lastname", userModel.Lastname),
            };

            claims.Add(new Claim("RoleId", userModel.RoleId.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, userModel.RoleName));
            
            return claims;
        }
    }
}
