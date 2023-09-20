using Microsoft.AspNetCore.Mvc;
using ToDosApi.Commands;
using ToDosApi.Exceptions;
using ToDosApi.Services;

namespace ToDosApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDoController : ControllerBase
{
    private readonly IToDoCommandHandler _toDoCommandHandler;

    public ToDoController(IToDoCommandHandler toDoCommandHandler)
    {
        _toDoCommandHandler = toDoCommandHandler;
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateToDoAsync(CreateToDo command)
    {
        await _toDoCommandHandler.HandleAsync(command);
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
    
    [HttpPut]
    [Route("/Name")]
    public async Task<ActionResult> UpdateToDoAsync(UpdateToDo command)
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