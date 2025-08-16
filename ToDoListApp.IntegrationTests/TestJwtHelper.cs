using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ToDoListApp.IntegrationTests;

public static class TestJwtHelper
{
    public static string GenerateJwt(string userId = "1")
    {
        const string key = "super_secret_key_here_123!_with_extra_chars_for_32bytes";
        const string issuer = "http://localhost:5103";
        const string audience = "http://localhost:7230";

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(ClaimTypes.NameIdentifier, userId)
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}