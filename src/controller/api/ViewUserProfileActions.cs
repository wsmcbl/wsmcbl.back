using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[Route("config/users")]
[ApiController]
public class ViewUserProfile(ViewUserController controller) : ActionsBase
{
    /// <summary>Get user information.</summary>
    /// <response code="200">Returns a user information.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">If the user not exist.</response>
    [ResourceAuthorizer("user:read")]
    [HttpGet]
    [Route("{userId}")]
    public async Task<IActionResult> getUser([Required] string userId)
    {
        var result = await controller.getUserById(userId);
        var nextCloudGroup = await controller.getNextCloudGroupByUser(result);
        
        return CreatedAtAction(null, result.mapToDto(nextCloudGroup));
    }
}