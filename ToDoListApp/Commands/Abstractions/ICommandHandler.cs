﻿using MediatR;
using ToDoListApp.Commands.Result;

namespace ToDoListApp.Commands.Abstractions;

public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, CommandResult<TResult>> where TCommand : ICommand<TResult>;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, CommandResult> where TCommand : ICommand;