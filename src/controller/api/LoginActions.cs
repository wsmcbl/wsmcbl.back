using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.config;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[Route("config")]
[ApiController]
public class LoginActions(LoginController controller) : ActionsBase
{
    /// <summary>
    ///  Returns token by credentials (login)
    /// </summary>
    /// <remarks>
    /// The token property can be null or empty.
    /// </remarks>
    /// <response code="200">Returns a token.</response>
    /// <response code="400">If the dto is not valid.</response>
    /// <response code="404">Resource depends on another resource not found.</response>
    [AllowAnonymous]
    [HttpPost]
    [Route("users/tokens")]
    public async Task<IActionResult> login(LoginDto dto)
    {
        var result = await controller.getTokenByCredentials(dto.toEntity());
        return Ok(result.mapToDto());
    }
    
    
    /// <summary>
    ///  Get user information
    /// </summary>
    /// <response code="200">Returns a user information.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">If the user not exist.</response>
    [ResourceAuthorizer("admin", "secretary", "cashier","teacher")]
    [HttpGet]
    [Route("users")]
    public async Task<IActionResult> getUser()
    {
        var result = await controller.getUserById(getAuthenticatedUserId());
        return CreatedAtAction(null, result.mapToDto());
    }
    
    /// <summary>
    ///  Update user information
    /// </summary>
    /// <response code="200">Returns a user new information.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">If the user not exist.</response>
    [ResourceAuthorizer("admin")]
    [HttpPut]
    [Route("users")]
    public async Task<IActionResult> updateUser([Required] UserDto userDto)
    {
        var result = await controller.updateUser(getAuthenticatedUserId(), userDto.toEntity());
        return CreatedAtAction(null, result.mapToDto());
    }
}
