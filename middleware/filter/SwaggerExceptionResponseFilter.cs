using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace wsmcbl.back.middleware.filter;

public class SwaggerExceptionResponseFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Responses.Add(HttpStatus.EntityNotFound.ToString(), new OpenApiResponse
        {
            Description = "Entity not found",
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["application/json"] = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Example = new OpenApiObject
                        {
                            ["message"] = new OpenApiString("Entity #EntityName and ID = #identifier not found.")
                        }
                    }
                }
            }
        });
    }
}