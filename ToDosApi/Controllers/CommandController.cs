using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using ToDosApi.Commands;
using ToDosApi.Exceptions;
using ToDosApi.Models;
using ToDosApi.Services;

namespace ToDosApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CommandController : ControllerBase
{
    private readonly ToDoCommandHandler _toDoCommandHandler;

    public CommandController(ToDoCommandHandler toDoCommandHandler)
    {
        _toDoCommandHandler = toDoCommandHandler;
    }

    [HttpPost]
    public async Task<ActionResult<ToDo>> CreateToDoAsync([FromBody] CreateToDo command)
    {
        try
        {
            var todo = await _toDoCommandHandler.Handle(command);
            return Ok(todo);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (ServerErrorException e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<DateTime>> CompleteToDoAsync([FromBody] CompleteToDo command)
    {
        try
        {
            var completedTime = await _toDoCommandHandler.Handle(command);
            return Ok(completedTime);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPut]
    [Route("Edit")]
    public async Task<ActionResult> CompleteToDoAsync([FromBody] UpdateToDo command)
    {
        try
        {
            await _toDoCommandHandler.Handle(command);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPatch]
    public async Task<ActionResult> Patch([FromRoute] int key, [FromBody] Delta<ToDo> delta)
    {
        try
        {
            await _toDoCommandHandler.Handle(key, delta);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpDelete]
    public async Task<ActionResult> RemoveToDoAsync([FromBody] RemoveToDo command)
    {
        try
        {
            await _toDoCommandHandler.Handle(command);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}