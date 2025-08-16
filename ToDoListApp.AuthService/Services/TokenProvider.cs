using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ToDoListApp.AuthService.Models;

namespace ToDoListApp.AuthService.Services;

public class TokenProvider(IConfiguration config) : ITokenProvider
{
    public string Create(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(config["Jwt:Key"]);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.UniqueName, user.UserName)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(config.GetValue<int>("Jwt:ExpirationInMinutes")),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            Audience = config["Jwt:Audience"],
            Issuer = config["Jwt:Issuer"]
        };

        var jwt = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(jwt);
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }
}