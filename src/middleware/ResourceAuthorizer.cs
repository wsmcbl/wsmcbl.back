using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace wsmcbl.src.middleware;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
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
        else if (hasPermissions(user) || hasRoles(user))   
        {
            return;
        }
        else
        {
            status = StatusCodes.Status403Forbidden;
            detail = "You do not have the necessary permissions to access this resource.";
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

    private bool hasRoles(ClaimsPrincipal user)
    {
        return _roles.Any(user.IsInRole);
    }

    private bool hasPermissions(ClaimsPrincipal user)
    {
        var permissionList = user.Claims
            .Where(c => c.Type == "Permission")
            .Select(c => c.Value).ToList();

        return _roles.Any(permission => permissionList.Contains(permission));
    }
}