namespace ToDoListApp.Commands.Result;

public class CommandResult : ICommandResult
{
    public bool Success { get; set; }
    public ErrorCode? ErrorCode { get; set; }
    public string? ErrorMessage { get; set; }

    public static CommandResult Ok() => new() { Success = true };

    public static CommandResult Fail(ErrorCode code, string message) => new()
    {
        Success = false,
        ErrorCode = code,
        ErrorMessage = message
    };
}

public class CommandResult<T> : ICommandResult
{
    public bool Success { get; set; }
    public ErrorCode? ErrorCode { get; set; }
    public string? ErrorMessage { get; set; }
    public T? Data { get; set; }

    public static CommandResult<T> Ok(T data) => new() { Success = true, Data = data };

    public static CommandResult<T> Fail(ErrorCode code, string message) => new()
    {
        Success = false,
        ErrorCode = code,
        ErrorMessage = message
    };
}