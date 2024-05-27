using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using wsmcbl.back.exception;

namespace wsmcbl.back.filter;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        if(context.Exception is EntityNotFoundException ex)
        {
            context.Result = new NotFoundObjectResult(new { message = ex.Message });
            context.ExceptionHandled = true;
        }
    }
}