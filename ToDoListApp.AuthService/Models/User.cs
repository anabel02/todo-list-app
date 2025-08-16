using Microsoft.AspNetCore.Identity;

namespace ToDoListApp.AuthService.Models;

public class User : IdentityUser
{
    public List<RefreshToken> RefreshTokens { get; set; } = [];
}