using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.exception;

namespace wsmcbl.src.middleware;

public class ApiExceptionHandler
{
    private readonly RequestDelegate _next;

    public ApiExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);

            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                throw new UnauthorizedException(
                    "Access denied. User does not have permissions to access this resource.");
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            await HandleExceptionAsync(context, ex, 401);
        }
        catch (BadHttpRequestException ex)
        {
            await HandleExceptionAsync(context, ex, ex.StatusCode);

        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, StatusCodes.Status500InternalServerError);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = "An error occurred while processing your request.",
            Detail = exception.Message,
            Extensions = new Dictionary<string, object?>
            {
                { "trace", exception.StackTrace }
            } 
        };

        return context.Response.WriteAsJsonAsync(problemDetails);
    }
}
