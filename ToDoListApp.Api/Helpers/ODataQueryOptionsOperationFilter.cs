using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ToDoListApp.Helpers;

public class ODataQueryOptionsOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.ApiDescription.HttpMethod != "GET") return;

        operation.Parameters = operation.Parameters
            .Where(p => !p.Name.StartsWith("options", StringComparison.OrdinalIgnoreCase))
            .ToList();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "$filter",
            In = ParameterLocation.Query,
            Description = "Filter results",
            Required = false,
            Schema = new OpenApiSchema { Type = "string" }
        });

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "$orderby",
            In = ParameterLocation.Query,
            Description = "Order results",
            Required = false,
            Schema = new OpenApiSchema { Type = "string" }
        });

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "$top",
            In = ParameterLocation.Query,
            Description = "Limit number of results",
            Required = false,
            Schema = new OpenApiSchema { Type = "integer", Format = "int32" }
        });

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "$skip",
            In = ParameterLocation.Query,
            Description = "Skip number of results",
            Required = false,
            Schema = new OpenApiSchema { Type = "integer", Format = "int32" }
        });
    }
}