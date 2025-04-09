using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.exception;

namespace wsmcbl.src.controller.api;

public class ActionsBase : ControllerBase
{
    protected static string getContentType(int value)
    {
        return value switch
        {
            1 => string.Empty,
            2 => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            _ => string.Empty
        };
    }
    
    protected string getAuthenticatedUserId()
    {
        var userId = User.FindFirstValue("userid");
        if (userId == null)
        {
            throw new EntityNotFoundException("UserEntity", userId);
        }

        return userId;
    }
}