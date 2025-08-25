using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Application.Abstractions;

namespace ToDoListApp.Extensions;

public static class CommandResultExtensions
{
    public static ActionResult<T> ToActionResult<T>(this CommandResult<T> result)
    {
        var statusCode = (int)result.StatusCode;
        object? body = result.Success ? result.Data : result.ErrorMessage ?? "An unknown error occurred";
        return new ObjectResult(body) { StatusCode = statusCode };
    }

    public static IActionResult ToActionResult(this CommandResult result)
    {
        var statusCode = (int)result.StatusCode;
        var body = result.Success ? null : result.ErrorMessage ?? "An unknown error occurred";
        return new ObjectResult(body) { StatusCode = statusCode };
    }
}