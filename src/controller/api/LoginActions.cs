using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;

namespace wsmcbl.src.controller.api;

[Route("users")]
[ApiController]
public class LoginActions(ILoginController controller) : ControllerBase
{
    /// <summary>
    ///  Returns token by credentials
    /// </summary>
    /// <response code="200">Returns a token.</response>
    [HttpGet]
    [Route("tokens")]
    public async Task<IActionResult> login(LoginDto dto)
    {
        return Ok(await controller.getTokenByCredentials(dto.toEntity()));
    }
}
