using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.AuthService.Models;

namespace ToDoListApp.AuthService.Services;

public class AuthService(UserManager<User> userManager, ITokenProvider tokenProvider, AuthDbContext context)
    : IAuthService
{
    public async Task<(string accessToken, string refreshToken)> RegisterAsync(string username, string password)
    {
        var user = new User { UserName = username, Email = username };
        var result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

        await userManager.AddToRoleAsync(user, AppRoles.User);

        return await GenerateTokensAsync(user);
    }

    public async Task<(string accessToken, string refreshToken)> LoginAsync(string username, string password)
    {
        var user = await userManager.FindByNameAsync(username);
        if (user == null || !await userManager.CheckPasswordAsync(user, password))
            throw new UnauthorizedAccessException("Invalid credentials");

        return await GenerateTokensAsync(user);
    }

    public async Task<(string accessToken, string refreshToken)> RefreshTokenAsync(string refreshToken)
    {
        var tokenEntity = await context.RefreshTokens
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Token == refreshToken && !t.Revoked);

        if (tokenEntity == null || tokenEntity.ExpiresAt < DateTime.UtcNow)
            throw new UnauthorizedAccessException("Invalid or expired refresh token");

        var newTokens = await GenerateTokensAsync(tokenEntity.User);

        tokenEntity.Token = newTokens.refreshToken;
        tokenEntity.ExpiresAt = DateTime.UtcNow.AddDays(7);
        await context.SaveChangesAsync();

        return newTokens;
    }

    public async Task RevokeTokensAsync(string userId)
    {
        await context.RefreshTokens
            .Where(x => x.UserId == userId)
            .ExecuteDeleteAsync();
    }

    private async Task<(string accessToken, string refreshToken)> GenerateTokensAsync(User user)
    {
        var accessToken = tokenProvider.Create(user);
        var refreshToken = new RefreshToken
        {
            UserId = user.Id,
            Token = tokenProvider.GenerateRefreshToken(),
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            CreatedAt = DateTime.UtcNow
        };

        context.RefreshTokens.Add(refreshToken);
        await context.SaveChangesAsync();

        return (accessToken, refreshToken.Token);
    }
}