using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UMS.API.Filters;

public class AddTenantHeaderParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "X-Tenant-ID",
            In = ParameterLocation.Header,
            Required = true, // Set to false if the header is optional
            Schema = new OpenApiSchema
            {
                Type = "string"
            }
        });
    }
}