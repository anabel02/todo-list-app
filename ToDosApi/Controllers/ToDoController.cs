using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using ToDosApi.Commands;
using ToDosApi.Exceptions;
using ToDosApi.Models;
using ToDosApi.Persistence;
using ToDosApi.Services;
using ODataController = Microsoft.AspNetCore.OData.Routing.Controllers.ODataController;

namespace ToDosApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDoController : ODataController
{
    private readonly ToDoCommandHandler _toDoCommandHandler;
    private readonly IToDoQueryHandler _toDoQueryHandler;
    private readonly ToDoContext _context;

    public ToDoController(ToDoCommandHandler toDoCommandHandler, IToDoQueryHandler toDoQueryHandler, ToDoContext context)
    {
        _toDoCommandHandler = toDoCommandHandler;
        _toDoQueryHandler = toDoQueryHandler;
        _context = context;
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
    [Microsoft.AspNetCore.OData.Query.EnableQuery]
    public IEnumerable<ToDo>? Get() => _context.ToDos;
    
    [HttpGet("{key:int}")]
    [Microsoft.AspNetCore.OData.Query.EnableQuery]
    public ActionResult<ToDo> Get([FromODataUri] int key)
    {
        return (ActionResult<ToDo>)_context.ToDos.Find(key) ?? NotFound();
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