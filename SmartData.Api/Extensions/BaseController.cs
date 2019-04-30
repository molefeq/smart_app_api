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
            buyDataModel.EmailAddress = "molefeq@gmail.com";// this.EmailAddress;
            buyDataModel.Firstname = "Qinisela";// this.Firstname;
            buyDataModel.Lastname = "Molefe";// this.Lastname;
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
