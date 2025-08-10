namespace ToDoListApp.Application.Abstractions
{
    public class PaginatedList<T>(IReadOnlyList<T> items, int totalCount, int pageNumber, int pageSize)
    {
        public IReadOnlyList<T> Items { get; } = items;
        public int TotalCount { get; } = totalCount;
        public int PageNumber { get; } = pageNumber;
        public int PageSize { get; } = pageSize;

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber * PageSize < TotalCount;
    }
}