using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.config;
using wsmcbl.src.middleware;
using wsmcbl.src.model;

namespace wsmcbl.src.controller.api;

[Route("config")]
[ApiController]
public class CreateUserActions(CreateUserController controller) : ActionsBase
{
    /// <summary>Get nextcloud group list.</summary>
    /// <response code="200">Return list, the list can be empty</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("nextcloud/groups")]
    [ResourceAuthorizer("user:read")]
    public async Task<IActionResult> getNextcloudGroupList()
    {
        return Ok(await controller.getNextcloudGroupList());
    }
    
    /// <summary>Get user paged list.</summary>
    /// <remarks>Values for sortBy: userId, roleId, name, email, isActive, createAt and updateAt.</remarks>
    /// <response code="200">Return a paged list, the list can be empty</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("users")]
    [ResourceAuthorizer("user:read")]
    public async Task<IActionResult> getUserList([FromQuery] PagedRequest request)
    {
        request.checkSortByValue(["userId", "roleId", "name", "email", "isActive", "createAt", "updateAt"]);
        
        var result = await controller.getUserList(request);

        var pagedResult = new PagedResult<UserToListDto>(result.data.mapToListDto());
        pagedResult.setup(result);
        
        return Ok(pagedResult);
    }
    
    /// <summary>Create new user.</summary>
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
    [ResourceAuthorizer("user:create")]
    public async Task<IActionResult> createUser(UserToCreateDto dto)
    {
        var group = dto.nextCloudGroup ?? string.Empty;
        var result = await controller.createUser(dto.toEntity(), group);
        return CreatedAtAction(null, new UserToCreateDto(result, group));
    }
}