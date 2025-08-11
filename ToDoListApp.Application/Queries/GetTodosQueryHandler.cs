using Microsoft.EntityFrameworkCore;
using ToDoListApp.Application.Abstractions;
using ToDoListApp.Application.Dtos;
using ToDoListApp.Persistence;

namespace ToDoListApp.Application.Queries;

public class GetTodosQueryHandler(ToDoContext context) : IQueryHandler<GetTodosQuery, ToDoDto>
{
    public async Task<PaginatedList<ToDoDto>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        var query = context.ToDos
            .ApplyFiltering(request.Parameters.Filter, t => t.Task)
            .ApplySorting(request.Parameters.Sorting)
            .ApplyPaging(request.Parameters.PageNumber, request.Parameters.PageSize);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Select(t => new ToDoDto(t.Id, t.Task, t.CreatedDateTime, t.CompletedDateTime))
            .ToListAsync(cancellationToken);

        return new PaginatedList<ToDoDto>(items, totalCount, request.Parameters.PageNumber, request.Parameters.PageSize);
    }
}