using System.Security.Claims;
using ToDoListApp.Application.Abstractions;

namespace ToDoListApp.Helpers;

public class HttpCurrentUser(IHttpContextAccessor accessor) : ICurrentUser
{
    public string? UserId
    {
        get
        {
            var user = accessor.HttpContext?.User;
            if (user is null) return null;

            return user.FindFirstValue(ClaimTypes.NameIdentifier) ?? user.FindFirstValue("sub");
        }
    }
}