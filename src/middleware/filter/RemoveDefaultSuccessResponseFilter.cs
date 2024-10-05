using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace wsmcbl.src.middleware.filter;

public class RemoveDefaultSuccessResponseFilter : IOperationFilter
{
    
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hasCustomResponses = operation.Responses.Any(r => r.Key != "200");

        if (hasCustomResponses && operation.Responses.ContainsKey("200"))
        {
            operation.Responses.Remove("200");
        }
    }
}