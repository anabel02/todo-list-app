using ToDoListApp.Application.Abstractions;
using ToDoListApp.Application.Dtos;
using ToDoListApp.Domain;
using ToDoListApp.Persistence;

namespace ToDoListApp.Application.Queries;

public class GetTodosQueryHandler(ToDoContext context) : ODataQueryHandler<ToDo, ToDoDto>(context);