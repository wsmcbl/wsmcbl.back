using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.config;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin")]
[Route("config")]
[ApiController]
public class AssignPermissionsActions(AssignPermissionsController controller) : ActionsBase
{
    /// <summary>Assign permissions and update user.</summary>
    /// <remarks>
    /// The secondName and secondSurname can be null or empty.
    /// The nextCloudGroup can be empty.
    /// </remarks>
    /// <response code="201">Returns a user updated.</response>
    /// <response code="400">If the dto is not valid.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">If the user not exist.</response>
    [HttpPut]
    [Route("users/{userId}")]
    public async Task<IActionResult> updateUser(UserToCreateDto dto, [Required] string userId)
    {
        var result = await controller.updateUser(dto.toEntity(), dto.nextCloudGroup);
        await controller.assignPermissions(dto.permissionList, (Guid)result.userId!);
        return Ok(new UserToCreateDto(result));
    }
}