using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ECommerce.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var id = user.FindFirstValue(JwtRegisteredClaimNames.Sub)
                     ?? throw new InvalidOperationException("User Id claim missing");
            return int.Parse(id);
        }
    }
}
