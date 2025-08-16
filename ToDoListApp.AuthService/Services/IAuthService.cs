namespace ToDoListApp.AuthService.Services;

public interface IAuthService
{
    Task<(string accessToken, string refreshToken)> RegisterAsync(string username, string password);
    Task<(string accessToken, string refreshToken)> LoginAsync(string username, string password);
    Task<(string accessToken, string refreshToken)> RefreshTokenAsync(string refreshToken);
    Task RevokeTokensAsync(string userId);
}