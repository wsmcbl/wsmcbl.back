using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using wsmcbl.back.exception;

namespace wsmcbl.back.middleware.filter;

public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        
        var statusCode = context.Exception switch
        {
            EntityNotFoundException => (int)HttpStatus.EntityNotFound,
            _ => (int)HttpStatusCode.InternalServerError
        };
        
        context.HttpContext.Response.StatusCode = statusCode;
        context.HttpContext.Response.ContentType = "application/json";
        context.ExceptionHandled = true;
        
        var response = new
        {
            message = context.Exception.Message, statusCode
        };

        context.Result = new JsonResult(response);
    }
}