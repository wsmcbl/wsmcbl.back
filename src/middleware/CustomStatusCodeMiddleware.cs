using System.Net;
using System.Text.Json;

namespace wsmcbl.back.middleware;

public class CustomStatusCodeMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        await next(context);

        if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                message = "Endpoint not found",
                statusCode = context.Response.StatusCode
            }));
        }
    }
}