using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.config;

namespace wsmcbl.src.controller.api;

[Route("users")]
[ApiController]
public class LoginActions(ILoginController controller) : ControllerBase
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
    [HttpPost]
    [Route("tokens")]
    public async Task<IActionResult> login(LoginDto dto)
    {
        var result = await controller.getTokenByCredentials(dto.toEntity());
        return Ok(result.mapToDto());
    }
    
    
    /// <summary>
    ///  Create new user 
    /// </summary>
    /// <remarks>
    /// The secondName and secondSurname can be null or empty.
    /// </remarks>
    /// <response code="201">Returns a new user created.</response>
    /// <response code="400">If the dto is not valid.</response>
    /// <response code="409">The email is duplicate.</response>
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> createUser(UserToCreateDto dto)
    {
        var result = await controller.createUser(dto.toEntity());
        return CreatedAtAction(null, result.mapToDto());
    }
}
