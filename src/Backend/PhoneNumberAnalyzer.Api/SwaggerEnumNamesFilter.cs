using System.Text.Json.Nodes;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PhoneNumberAnalyzer.Api;

public class SwaggerEnumNamesFilter : ISchemaFilter
{
    public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
    {
        if (!context.Type.IsEnum) return;
        if (schema is not OpenApiSchema openApiSchema) return; // Extensions is only mutable on the concrete type

        var names = Enum.GetNames(context.Type);
        var array = new JsonArray();
        foreach (var name in names)
        {
            array.Add(JsonValue.Create(name));
        }

        openApiSchema.Extensions ??= new Dictionary<string, IOpenApiExtension>();
        openApiSchema.Extensions["x-enumNames"] = new JsonNodeExtension(array);
    }
}
