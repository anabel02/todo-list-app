using System.Web.Http;
using Microsoft.AspNet.OData;
using ToDoListApp.Models;
using ToDoListApp.Persistence;

namespace ToDoListApp.Controllers;

public class ToDosController(ToDoContext context) : ODataController
{
    [EnableQuery]
    public IQueryable<ToDo> Get() => context.ToDos;

    [EnableQuery]
    public SingleResult<ToDo> Get([FromODataUri] int key) => new(context.ToDos.Where(v => v.Id == key));
}