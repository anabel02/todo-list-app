namespace ToDoListApp.Application.Abstractions;

using System.ComponentModel.DataAnnotations;

public class QueryParameters
{
    [Range(1, int.MaxValue, ErrorMessage = "PageNumber must be greater than 0.")]
    public int PageNumber { get; set; } = 1;

    [Range(1, int.MaxValue, ErrorMessage = "PageSize must be greater than 0.")]
    public int PageSize { get; set; } = 10;

    [Required] public SortingParams Sorting { get; set; } = new();
    [Required] public FilterParams Filter { get; set; } = new();
}

public class SortingParams
{
    [StringLength(50, ErrorMessage = "OrderBy must be 50 characters or fewer.")]
    public string OrderBy { get; set; } = string.Empty;

    public bool Descending { get; set; } = false;
}

public class FilterParams
{
    [StringLength(100, ErrorMessage = "Search term must be 100 characters or fewer.")]
    public string SearchTerm { get; set; } = string.Empty;

    public List<FieldFilter> FieldFilters { get; set; } = [];
}

public class FieldFilter
{
    public string FieldName { get; set; } = string.Empty;
    public object? Value { get; set; }
    public FilterOperator Operator { get; set; } = FilterOperator.Equal;
}

public enum FilterOperator
{
    Equal,
    NotEqual
}