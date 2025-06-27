using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Commands;
using ToDoListApp.Models;
using ToDoListApp.Services;

namespace ToDoListApp.Controllers;

[ApiController]
[Route("[controller]")]
public class CommandController(ToDoService toDoService) : BaseController
{
    [HttpPost]
    public async Task<ActionResult<ToDo>> CreateToDoAsync([FromBody] CreateToDo request)
    {
        var result = await toDoService.Handle(request);
        return FromResult(result);
    }

    [HttpPut]
    public async Task<ActionResult<DateTime>> CompleteToDoAsync([FromBody] CompleteToDo request)
    {
        var result = await toDoService.Handle(request);
        return FromResult(result);
    }

    [HttpPut]
    [Route("Edit")]
    public async Task<IActionResult> CompleteToDoAsync([FromBody] UpdateToDo request)
    {
        var result = await toDoService.Handle(request);
        return FromResult(result);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveToDoAsync([FromBody] RemoveToDo request)
    {
        var result = await toDoService.Handle(request);
        return FromResult(result);
    }
}