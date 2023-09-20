using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ToDosApi.Commands;
using ToDosApi.Exceptions;
using ToDosApi.Services;

namespace ToDosApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDoController : ControllerBase
{
    private readonly ToDoCommandHandler _toDoCommandHandler;
    private readonly IToDoQueryHandler _toDoQueryHandler;

    public ToDoController(ToDoCommandHandler toDoCommandHandler, IToDoQueryHandler toDoQueryHandler)
    {
        _toDoCommandHandler = toDoCommandHandler;
        _toDoQueryHandler = toDoQueryHandler;
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

    [HttpGet]
    public Task<IActionResult> GetToDos() => 
        Task.FromResult<IActionResult>(Ok(_toDoQueryHandler.All()));
    
    [HttpGet("/{id:int}")]
    public async Task<IActionResult> GetToDos(int id)
    {
        try
        {
            return Ok(await _toDoQueryHandler.FindById(id));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpGet]
    [Route("/Completed")]
    public Task<IActionResult> GetToDosFilterByIsComplete() => 
        Task.FromResult<IActionResult>(Ok(_toDoQueryHandler.FilterByIsComplete()));
    
    [HttpGet]
    [Route("/NotCompleted")]
    public Task<IActionResult> GetToDosFilterByIsNotComplete() => 
        Task.FromResult<IActionResult>(Ok(_toDoQueryHandler.FilterByIsNotComplete()));
    
    [HttpGet]
    [Route("/Task")]
    public Task<IActionResult> GetToDosFilterByTask([Required] string name) => 
        Task.FromResult<IActionResult>(Ok(_toDoQueryHandler.FindByName(name)));
}