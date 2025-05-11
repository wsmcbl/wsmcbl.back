using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.config;

namespace wsmcbl.src.controller.api;

[Route("config/users")]
[ApiController]
public class LoginActions(LoginController controller) : ActionsBase
{
    /// <summary>Returns token by credentials (login).</summary>
    /// <remarks>The token property can be null or empty.</remarks>
    /// <response code="200">Returns a token.</response>
    /// <response code="400">If the dto is not valid.</response>
    /// <response code="401">If the user cannot be authenticated.</response>
    /// <response code="404">If a resource depends on another resource not found.</response>
    /// <response code="409">If the user is disabled.</response>
    [AllowAnonymous]
    [HttpPost]
    [Route("tokens")]
    public async Task<IActionResult> login(LoginDto dto)
    {
        var result = await controller.getTokenByCredentials(dto.toEntity());
        return Ok(result.mapToDto());
    }
}
