using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;

namespace wsmcbl.src.controller.api;

[Route("academy")]
[ApiController]
public class ViewGradeOnlineActions(ViewGradeOnlineController controller) : ControllerBase
{
    /// <summary>
    ///  Return true or false if the student is valid.
    /// </summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Student not found.</response>
    [HttpGet]
    [Route("students/{studentId}")]
    public async Task<IActionResult> validateStudent(string studentId, [FromQuery] string token)
    {
        await controller.validateStudent(studentId, token);
        return Ok();
    }
}