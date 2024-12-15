using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.exception;

namespace wsmcbl.src.controller.api;

public class ActionsBase : ControllerBase
{
    protected string getAuthenticatedUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            throw new EntityNotFoundException("user", userId);
        }

        return userId;
    }
    
    protected string[] validateQueryValue(string q)
    {
        if (string.IsNullOrWhiteSpace(q))
        {
            throw new BadRequestException("Query parameter 'q' is required.");
        }
        
        var parts = q.Split(':');
        if (parts.Length != 2)
        {
            throw new BadRequestException("Query parameter 'q' is not in the correct format.");
        }

        return parts;
    }
}