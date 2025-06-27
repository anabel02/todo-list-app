namespace ToDoListApp.Commands.Result;

public interface ICommandResult
{
    bool Success { get; }
    ErrorCode? ErrorCode { get; }
    string? ErrorMessage { get; }
}