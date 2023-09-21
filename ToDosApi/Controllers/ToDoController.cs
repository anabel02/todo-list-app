using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using ToDosApi.Commands;
using ToDosApi.Exceptions;
using ToDosApi.Models;
using ToDosApi.Services;

namespace ToDosApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDoController : ControllerBase
{
    private readonly ToDoCommandHandler _toDoCommandHandler;

    public ToDoController(ToDoCommandHandler toDoCommandHandler)
    {
        _toDoCommandHandler = toDoCommandHandler;
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateToDoAsync(CreateToDo command)
    {
        await _toDoCommandHandler.HandleAsync(command);
        return Ok();
    }
    
    [HttpPut]
    public async Task<ActionResult> CompleteToDoAsync(CompleteToDo command)
    {
        try
        {
            await _toDoCommandHandler.HandleAsync(command);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        return Ok();
    }
    
    [HttpPatch]
    public async Task<ActionResult> Patch([FromRoute] int key, [FromBody] Delta<ToDo> delta)
    {
        try
        {
            await _toDoCommandHandler.HandleAsync(key, delta);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        return Ok();
    }
    
    [HttpDelete]
    public async Task<ActionResult> RemoveToDoAsync(RemoveToDo command)
    {
        try
        {
            await _toDoCommandHandler.HandleAsync(command);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        
        return Ok();
    }
}