using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace wsmcbl.src.middleware;

public class ResourceAuthorizer : ActionFilterAttribute
{
    private readonly string[] _roles;

    public ResourceAuthorizer(params string[] roles)
    {
        _roles = roles;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var user = context.HttpContext.User;

        int status;
        string detail;
        
        if (!user.Identity!.IsAuthenticated)
        {
            status = StatusCodes.Status401Unauthorized;
            detail = "User not identified.";
        }
        else if (!_roles.Any(role => user.IsInRole(role)))
        {
            status = StatusCodes.Status403Forbidden;
            detail = "You do not have the necessary permissions to access this resource.";
        }
        else if (!hasPermission(user))
        {
            status = StatusCodes.Status403Forbidden;
            detail = "You do not have the necessary permissions to access this resource (TEST).";
        }
        else
        {
            return;
        }
        
        var problemDetails = new ProblemDetails
        {
            Status = status,
            Title = "Access Denied.",
            Detail = detail
        };

        context.Result = new ObjectResult(problemDetails)
        {
            StatusCode = status
        };
    }

    private bool hasPermission(ClaimsPrincipal user)
    {
        var permissions = user.Claims
            .Where(c => c.Type == "Permission")
            .Select(c => c.Value);

        return permissions.Contains(_roles.ToList().ToString()!);
    }
}