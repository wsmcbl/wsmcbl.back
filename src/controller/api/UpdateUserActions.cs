using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.config;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[Route("config")]
[ApiController]
public class UpdateUserActions(UpdateUserController controller) : ActionsBase
{
    /// <summary>Get permission list.</summary>
    /// <response code="200">Return list, the list can be empty</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("permissions")]
    [Authorizer("permission:read")]
    public async Task<IActionResult> getPermissionList()
    {
        var result = await controller.getPermissionList();
        return Ok(result.mapToListDto());
    }
    
    /// <summary>Assign permissions and update user.</summary>
    /// <remarks>
    /// The secondName and secondSurname can be null or empty.
    /// </remarks>
    /// <response code="201">Returns a user updated.</response>
    /// <response code="400">If the dto is not valid.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">If the user not exist.</response>
    [HttpPut]
    [Route("users/{userId}")]
    [Authorizer("user:update")]
    public async Task<IActionResult> updateUser([Required] string userId, UserToUpdateDto dto)
    {
        var result = await controller.updateUser(dto.toEntity(userId), dto.nextCloudGroup ?? string.Empty);
        
        await controller.assignPermissions(result, dto.getUserPermissionList(userId));

        var nextCloudGroup = await controller.getNextCloudGroup(result);
        var response = new UserDto(result, nextCloudGroup);

        return Ok(response);
    }
    
    /// <summary>Update user password by id.</summary>
    /// <response code="201">Returns a user updated.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">If the user not exist.</response>
    [HttpPut]
    [Route("users/{userId}/passwords")]
    [Authorizer("user:update")]
    public async Task<IActionResult> updateUserPassword([Required] string userId)
    {
        var result = await controller.updateUserPassword(userId);
        return Ok(new UserToCreateDto(result, string.Empty));
    }
}