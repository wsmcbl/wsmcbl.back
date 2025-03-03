using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[Route("config")]
[ApiController]
public class UpdateRolesActions(UpdateRolesController controller) : ActionsBase
{
    /// <summary>Get roles list.</summary>
    /// <response code="200">Return a list, the list can be empty</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("roles")]
    [ResourceAuthorizer("rol:read")]
    public async Task<IActionResult> getRoleList()
    {
        return Ok(await controller.getRoleList());
    }    
}