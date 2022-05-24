using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI_PL.Controllers;

public static class Utils
{
    public static string? GetJWTTokenFromRequest(HttpRequest httpRequest)
    {
        var array = httpRequest.Headers.Authorization;

        foreach (var s in array.Where(s => !s.IsNullOrEmpty()))
        {
            var split = s.Split(' ');
            if (split.Length == 2 && split[0].Equals("bearer") && !split[1].IsNullOrEmpty()) return split[1];
        }

        return null;
    }


    public static int? GetUserIDFromJWT(HttpRequest httpRequest)
    {
        var jwtTokenString = GetJWTTokenFromRequest(httpRequest);
        var token = new JwtSecurityToken(jwtTokenString);

        var claim = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid);
        if (claim == null) return null;
        var v = claim.Value;
        if (v.IsNullOrEmpty()) return null;

        if (int.TryParse(v, out var intID)) return intID;

        return null;
    }
}