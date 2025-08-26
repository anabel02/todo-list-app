using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using ToDoListApp.Application.Abstractions;
using ToDoListApp.Application.Commands;
using ToDoListApp.Application.Dtos;
using ToDoListApp.Domain;
using ToDoListApp.Helpers;

namespace ToDoListApp.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ToDosController(ISender sender, ICurrentUser currentUser) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDoDto>>> Get(ODataQueryOptions<ToDo> options)
    {
        var request = new ODataQuery<ToDo, ToDoDto>(options);

        request = request.WithFilter(x => x.Profile.UserId == currentUser.UserId);

        var result = await sender.Send(request);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ToDoDto>> GetById([FromRoute] int id, ODataQueryOptions<ToDo> options)
    {
        var request = new SingleODataQuery<ToDo, ToDoDto>(options)
            .WithFilter(x => x.Id == id);

        request = request.WithFilter(x => x.Profile.UserId == currentUser.UserId);

        var result = await sender.Send(request);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ToDoDto>> Create([FromBody] CreateTaskCommandBody body)
    {
        var request = new CreateTaskCommand(body);
        var result = await sender.Send(request);
        return result.ToActionResult();
    }

    [HttpPut("{id:int}/complete")]
    public async Task<ActionResult<ToDoDto>> Complete([FromRoute] int id)
    {
        var request = new CompleteTaskCommand(id);
        var result = await sender.Send(request);
        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ToDoDto>> Update([FromRoute] int id, [FromBody] UpdateTaskBody body)
    {
        var request = new UpdateTaskCommand(id, body);
        var result = await sender.Send(request);
        return result.ToActionResult();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remove([FromRoute] int id)
    {
        var request = new RemoveTaskCommand(id);
        var result = await sender.Send(request);
        return result.ToActionResult();
    }
}