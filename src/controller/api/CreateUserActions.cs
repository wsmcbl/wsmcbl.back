using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.config;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin")]
[Route("management")]
[ApiController]
public class CreateUserActions(CreateUserController controller) : ActionsBase
{
    /// <summary>
    /// Get user list
    /// </summary>
    /// <response code="200">Return list, the list can be empty</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("users")]
    public async Task<IActionResult> getUserList()
    {
        var result = await controller.getUserList();
        return Ok(result.mapToListDto());
    }
    
    /// <summary>
    /// Get permission list
    /// </summary>
    /// <response code="200">Return list, the list can be empty</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("permissions")]
    public async Task<IActionResult> getPermissionList()
    {
        return Ok(await controller.getPermissionList());
    }
    
    /// <summary>
    ///  Create new user 
    /// </summary>
    /// <remarks>
    /// The secondName and secondSurname can be null or empty.
    /// The nextCloudGroup can be empty.
    /// </remarks>
    /// <response code="201">Returns a new user created.</response>
    /// <response code="400">If the dto is not valid.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="409">The email is duplicate.</response>
    [HttpPost]
    [Route("users")]
    public async Task<IActionResult> createUser(UserToCreateDto dto)
    {
        var result = await controller.createUser(dto.toEntity(), dto.nextCloudGroup);
        await controller.addPermissions(dto.permissionList, (Guid)result.userId!);
        return CreatedAtAction(null, result.mapToDto());
    }
}