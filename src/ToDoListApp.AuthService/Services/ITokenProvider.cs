using ToDoListApp.AuthService.Models;

namespace ToDoListApp.AuthService.Services;

public interface ITokenProvider
{
    string Create(User user);
    string GenerateRefreshToken();
}