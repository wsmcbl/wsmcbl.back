using System.Net;
using System.Text.Json;

namespace wsmcbl.back.middleware;

public class CustomStatusCodeMiddleware
{
    private readonly RequestDelegate _next;

    public CustomStatusCodeMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        var message = context.Response.StatusCode switch
        {
            (int)HttpStatusCode.NotFound => "Endpoint not found",
            (int)HttpStatusCode.InternalServerError => "An unexpected error occurred",
            _ => ""
        };
        
        if(message == "")
        {
            return;
        }

        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(new
        {
            message,
            statusCode = context.Response.StatusCode
        }));
    }
}