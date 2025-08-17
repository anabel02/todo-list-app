using System.Reflection;

namespace ToDoListApp.Application.Abstractions;

public static class MappingExtensions
{
    public static TDestination MapTo<TDestination>(this object? source)
        where TDestination : new()
    {
        ArgumentNullException.ThrowIfNull(source);

        var destination = new TDestination();
        var sourceType = source.GetType();
        var destType = typeof(TDestination);

        var destProperties = destType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanWrite);

        foreach (var destProp in destProperties)
        {
            var sourceProp = sourceType.GetProperty(destProp.Name, BindingFlags.Public | BindingFlags.Instance);
            if (sourceProp == null) continue;
            if (!sourceProp.CanRead) continue;

            if (!destProp.PropertyType.IsAssignableFrom(sourceProp.PropertyType))
                continue;

            var value = sourceProp.GetValue(source);
            destProp.SetValue(destination, value);
        }

        return destination;
    }
}