using ToDoListApp.Application.Abstractions;
using ToDoListApp.Application.Dtos;
using ToDoListApp.Domain;
using ToDoListApp.Persistence;

namespace ToDoListApp.Application.Queries;

public class GetSingleTodoQueryHandler(ToDoContext context) : SingleODataQueryHandler<ToDo, ToDoDto>(context);