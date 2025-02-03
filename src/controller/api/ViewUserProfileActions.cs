using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.config;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[Route("config")]
[ApiController]
public class ViewUserProfileActions(ViewUserProfileController controller) : ActionsBase
{
    /// <summary>Get user information.</summary>
    /// <response code="200">Returns a user information.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">If the user not exist.</response>
    [ResourceAuthorizer("user:read")]
    [HttpGet]
    [Route("users/{userId}")]
    public async Task<IActionResult> getUser([Required] string userId)
    {
        var result = await controller.getUserById(userId);
        var nextCloudGroup = await controller.getNextCloudGroupByUser(result);
        
        return CreatedAtAction(null, result.mapToDto(nextCloudGroup));
    }
    
    /// <summary>Get roles list.</summary>
    /// <response code="200">Returns a list information, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [ResourceAuthorizer("rol:read")]
    [HttpGet]
    [Route("roles")]
    public async Task<IActionResult> getRolesList([Required] string userId)
    {
        return Ok(await controller.getRolesList());
    }
}