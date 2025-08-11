using System.Text;

namespace ToDoListApp.Application.Abstractions;

public static class FilterParamsParser
{
    private static readonly Dictionary<FilterOperator, string> OperatorMap = new()
    {
        { FilterOperator.Equal, "eq" },
        { FilterOperator.NotEqual, "ne" }
    };

    private static readonly Dictionary<string, FilterOperator> ReverseOperatorMap =
        OperatorMap.ToDictionary(kv => kv.Value, kv => kv.Key, StringComparer.OrdinalIgnoreCase);

    public static IEnumerable<FieldFilter> GetFilters(this FilterParams filterParams)
    {
        return ParseFilterString(filterParams.FilterString);
    }

    public static string ToFilterString(FilterParams filterParams)
    {
        var filters = filterParams.GetFilters().ToList();

        if (!filters.Any())
            return string.Empty;

        var parts = filters.Select(f =>
        {
            var opStr = OperatorMap[f.Operator];
            var valueStr = f.Value?.ToString() ?? string.Empty;

            // Quote if it contains spaces or semicolon
            if (valueStr.Contains(' ') || valueStr.Contains(';'))
                valueStr = $"\"{valueStr.Replace("\"", "\\\"")}\""; // escape quotes

            return $"{f.FieldName} {opStr} {valueStr}";
        });

        return string.Join(";", parts);
    }

    public static IEnumerable<FieldFilter> ParseFilterString(string filterString)
    {
        if (string.IsNullOrWhiteSpace(filterString))
            yield break;

        var filters = filterString.Split(';', StringSplitOptions.RemoveEmptyEntries);

        foreach (var f in filters)
        {
            var parts = SplitPreservingQuotes(f.Trim());

            if (parts.Length < 3)
                continue; // invalid format

            var fieldName = parts[0];
            var opStr = parts[1];
            var valueStr = string.Join(' ', parts.Skip(2));

            if (!ReverseOperatorMap.TryGetValue(opStr, out var op))
                continue; // unknown operator

            // Remove surrounding quotes if present
            if (valueStr.StartsWith("\"") && valueStr.EndsWith("\""))
                valueStr = valueStr[1..^1].Replace("\\\"", "\"");

            object? value = null;
            if (valueStr != "null")
                value = valueStr;

            yield return new FieldFilter
            {
                FieldName = fieldName,
                Operator = op,
                Value = value
            };
        }
    }

    private static string[] SplitPreservingQuotes(string input)
    {
        var result = new List<string>();
        var current = new StringBuilder();
        var inQuotes = false;

        for (var i = 0; i < input.Length; i++)
        {
            var c = input[i];

            if (c == '"' && (i == 0 || input[i - 1] != '\\'))
            {
                inQuotes = !inQuotes;
                current.Append(c);
            }
            else if (!inQuotes && char.IsWhiteSpace(c))
            {
                if (current.Length <= 0)
                    continue;
                result.Add(current.ToString());
                current.Clear();
            }
            else
            {
                current.Append(c);
            }
        }

        if (current.Length > 0)
            result.Add(current.ToString());

        return result.ToArray();
    }
}