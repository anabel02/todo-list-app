using ToDoListApp.Application.Abstractions;
using ToDoListApp.Application.Dtos;

namespace ToDoListApp.Application.Commands;

public record CreateProfileCommand : ICommand<ProfileDto>;