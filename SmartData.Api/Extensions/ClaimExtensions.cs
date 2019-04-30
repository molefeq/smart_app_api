using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
namespace SmartData.Api.Extensions
{
    public static class ClaimExtensions
    {
        public static Claim GetClaimType(this IEnumerable<Claim> claims, string claimType)
        {
            return claims.Where(c => c.Type == claimType).FirstOrDefault();
        }
    }
}
