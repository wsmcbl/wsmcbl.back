using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.exception;

namespace wsmcbl.src.controller.api;

public class ActionsBase : ControllerBase
{
    protected string getAuthenticatedUserId()
    {
        var userId = User.FindFirstValue("userid");
        if (userId == null)
        {
            throw new EntityNotFoundException("user", userId);
        }

        return userId;
    }
}