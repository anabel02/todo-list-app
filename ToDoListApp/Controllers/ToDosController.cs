using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using ToDoListApp.Models;
using ToDoListApp.Persistence;

namespace ToDoListApp.Controllers;

public class ToDosController(ToDoContext context) : ControllerBase
{
    [EnableQuery]
    public IQueryable<ToDo> Get() => context.ToDos;

    [EnableQuery]
    public ActionResult<ToDo> Get([FromODataUri] int key)
    {
        var todo = context.ToDos.Find(key);
        return todo is not null ? Ok(todo) : NotFound();
    }
}