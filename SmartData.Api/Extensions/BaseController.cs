using Microsoft.AspNetCore.Mvc;
using SmartData.Data.ViewModels;
using SqsLibraries.Common.Extensions;

using System.Security.Claims;

namespace SmartData.Api.Extensions
{
    public class BaseController : ControllerBase
    {
        protected virtual ClaimsIdentity CurrentUser
        {
            get { return User.Identity as ClaimsIdentity; }
        }

        protected virtual void SetBuyerDetails(BuyDataModel buyDataModel)
        {
            buyDataModel.EmailAddress = this.EmailAddress;
            buyDataModel.Firstname = this.Firstname;
            buyDataModel.Lastname = this.Lastname;
            buyDataModel.UserId = this.UserId.Value;
        }

        protected virtual long? UserId
        {
            get
            {
                Claim userIdClaim = User.Claims.GetClaimType(ClaimTypes.Name);

                if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
                {
                    return null;
                }

                return userIdClaim.Value.ToLong();
            }
        }

        protected virtual string EmailAddress
        {
            get
            {
                Claim claim = User.Claims.GetClaimType("Username");

                if (claim == null || string.IsNullOrEmpty(claim.Value))
                {
                    return null;
                }

                return claim.Value;
            }
        }

        protected virtual string Firstname
        {
            get
            {
                Claim claim = User.Claims.GetClaimType("Firstname");

                if (claim == null || string.IsNullOrEmpty(claim.Value))
                {
                    return null;
                }

                return claim.Value;
            }
        }

        protected virtual string Lastname
        {
            get
            {
                Claim claim = User.Claims.GetClaimType("Lastname");

                if (claim == null || string.IsNullOrEmpty(claim.Value))
                {
                    return null;
                }

                return claim.Value;
            }
        }
    }
}
