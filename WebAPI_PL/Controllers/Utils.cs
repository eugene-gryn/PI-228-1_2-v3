using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI_PL.Controllers;

public static class Utils
{
    public static int? GetUserIDFromJWT(ClaimsPrincipal user)
    {
        var claim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid);
        if (claim == null) return null;
        var v = claim.Value;
        if (v.IsNullOrEmpty()) return null;

        if (int.TryParse(v, out var intID))
        {
            return intID;
        }

        return null;

    }
}