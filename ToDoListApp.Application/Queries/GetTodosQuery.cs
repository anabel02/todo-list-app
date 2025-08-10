using ToDoListApp.Application.Abstractions;
using ToDoListApp.Application.Dtos;
using ToDoListApp.Domain;

namespace ToDoListApp.Application.Queries;

public class GetTodosQuery(QueryParameters parameters) : Query<ToDoDto>(parameters);