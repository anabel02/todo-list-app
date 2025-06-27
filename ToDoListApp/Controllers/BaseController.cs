using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Commands.Result;

namespace ToDoListApp.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected ActionResult<T> FromResult<T>(CommandResult<T> result)
    {
        if (result.Success)
            return Ok(result.Data);

        if (result.ErrorMessage is null)
            return StatusCode(500, "An unknown error occurred");

        return result.ErrorCode switch
        {
            ErrorCode.NotFound => NotFound(result.ErrorMessage),
            ErrorCode.ValidationError => BadRequest(result.ErrorMessage),
            ErrorCode.Conflict => Conflict(result.ErrorMessage),
            ErrorCode.Unauthorized => Unauthorized(result.ErrorMessage),
            ErrorCode.Forbidden => Forbid(result.ErrorMessage),
            _ => StatusCode(500, result.ErrorMessage)
        };
    }

    protected IActionResult FromResult(CommandResult result)
    {
        if (result.Success)
            return Ok();

        if (result.ErrorMessage is null)
            return StatusCode(500, "An unknown error occurred");

        return result.ErrorCode switch
        {
            ErrorCode.NotFound => NotFound(result.ErrorMessage),
            ErrorCode.ValidationError => BadRequest(result.ErrorMessage),
            ErrorCode.Unauthorized => Unauthorized(result.ErrorMessage),
            ErrorCode.Forbidden => Forbid(result.ErrorMessage),
            ErrorCode.Conflict => Conflict(result.ErrorMessage),
            _ => StatusCode(500, result.ErrorMessage)
        };
    }
}