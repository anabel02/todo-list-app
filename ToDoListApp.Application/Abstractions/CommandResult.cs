using System.Net;

namespace ToDoListApp.Application.Abstractions;

public abstract class CommandResultBase
{
    public HttpStatusCode StatusCode { get; init; }
    public string? ErrorMessage { get; init; }
    public bool Success => (int)StatusCode >= 200 && (int)StatusCode < 300;
}

public class CommandResult : CommandResultBase
{
    public static CommandResult Ok() => new() { StatusCode = HttpStatusCode.OK };

    public static CommandResult Fail(HttpStatusCode code, string message) => new()
    {
        StatusCode = code,
        ErrorMessage = message
    };
}

public class CommandResult<T> : CommandResultBase
{
    public T? Data { get; init; }

    public static CommandResult<T> Ok(T data) => new() { StatusCode = HttpStatusCode.OK, Data = data };

    public static CommandResult<T> Fail(HttpStatusCode code, string message) => new()
    {
        StatusCode = code,
        ErrorMessage = message
    };
}