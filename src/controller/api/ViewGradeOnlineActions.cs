using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;

namespace wsmcbl.src.controller.api;

[Route("academy")]
[ApiController]
public class ViewGradeOnlineActions(ViewGradeOnlineController controller) : ControllerBase
{
    [HttpGet]
    [Route("students/{studentId}")]
    public async Task<IActionResult> validateStudent(string studentId, [FromQuery] string token)
    {
        await controller.validateStudent(studentId, token);
        return Ok();
    }
}