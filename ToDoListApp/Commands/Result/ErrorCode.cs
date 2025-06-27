namespace ToDoListApp.Commands.Result;

public enum ErrorCode
{
    NotFound,
    Conflict,
    ValidationError,
    Unauthorized,
    Forbidden,
    InternalError,
}