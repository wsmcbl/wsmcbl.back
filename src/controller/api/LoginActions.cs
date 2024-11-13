using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;

namespace wsmcbl.src.controller.api;

[Route("users")]
[ApiController]
public class LoginActions(ILoginController controller) : ControllerBase
{
    
}
