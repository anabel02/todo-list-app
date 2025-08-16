using Microsoft.AspNetCore.Mvc;
using ToDoListApp.AuthService.Services;

namespace ToDoListApp.AuthService.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] string username, [FromForm] string password)
    {
        var (accessToken, refreshToken) = await authService.RegisterAsync(username, password);
        return Ok(new
        {
            access_token = accessToken,
            refresh_token = refreshToken,
            token_type = "Bearer"
        });
    }

    [HttpPost("token")]
    public async Task<IActionResult> Token([FromForm] string username, [FromForm] string password)
    {
        var (accessToken, refreshToken) = await authService.LoginAsync(username, password);
        return Ok(new
        {
            access_token = accessToken,
            refresh_token = refreshToken,
            token_type = "Bearer"
        });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromForm] string refreshToken)
    {
        var (newAccessToken, newRefreshToken) = await authService.RefreshTokenAsync(refreshToken);
        return Ok(new
        {
            access_token = newAccessToken,
            refresh_token = newRefreshToken
        });
    }

    [HttpDelete("revoke")]
    public async Task<IActionResult> Revoke([FromForm] string userId)
    {
        await authService.RevokeTokensAsync(userId);
        return NoContent();
    }
}