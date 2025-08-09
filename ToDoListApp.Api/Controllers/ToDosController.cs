using MediatR;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Application.Commands;
using ToDoListApp.Domain;
using ToDoListApp.Helpers;
using ToDoListApp.Persistence;

namespace ToDoListApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDosController(ToDoContext context, ISender sender) : ODataController
{
    [EnableQuery]
    public IQueryable<ToDo> Get() => context.ToDos;

    [EnableQuery]
    public System.Web.Http.SingleResult<ToDo> Get([FromODataUri] int key) => new(context.ToDos.Where(v => v.Id == key));

    [HttpPost]
    public async Task<ActionResult<ToDo>> Create([FromBody] CreateTaskCommand request)
    {
        var result = await sender.Send(request);
        return result.ToActionResult();
    }

    [HttpPut("{id:int}/complete")]
    public async Task<ActionResult<DateTime>> Complete([FromRoute] int id)
    {
        var result = await sender.Send(new CompleteTaskCommand(id));
        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTaskCommand.UpdateToDoBody body)
    {
        var request = new UpdateTaskCommand(id, body);
        var result = await sender.Send(request);
        return result.ToActionResult();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remove([FromRoute] int id)
    {
        var result = await sender.Send(new RemoveTaskCommand(id));
        return result.ToActionResult();
    }
}