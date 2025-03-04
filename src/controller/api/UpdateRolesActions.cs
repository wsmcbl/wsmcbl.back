using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[Route("config/roles")]
[ApiController]
public class UpdateRolesActions(UpdateRolesController controller) : ActionsBase
{
    /// <summary>Get roles list.</summary>
    /// <response code="200">Return a list, the list can be empty</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("")]
    [ResourceAuthorizer("rol:read")]
    public async Task<IActionResult> getRoleList()
    {
        return Ok(await controller.getRoleList());
    }   
    
    /// <summary>Get roles by id.</summary>
    /// <response code="201">Returns a role.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">If the role not exist.</response>
    [HttpGet]
    [Route("{roleId:int}")]
    [ResourceAuthorizer("rol:read")]
    public async Task<IActionResult> getRoleById(int roleId)
    {
        return Ok(await controller.getRoleById(roleId));
    }   
    
    /// <summary>Update roles by id.</summary>
    /// <response code="201">Returns a role updated.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">If the role not exist.</response>
    [HttpPut]
    [Route("{roleId:int}")]
    [ResourceAuthorizer("rol:update")]
    public async Task<IActionResult> updateRole(int roleId, [Required] RoleToUpdateDto dto)
    {
        var result = await controller.updateRole(dto.toEntity(roleId));
        return Ok(result);
    }    
}