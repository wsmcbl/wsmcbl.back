using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin","secretary")]
[Route("secretary/")]
[ApiController]
public class MoveStudentFromEnrollmentActions(IMoveStudentFromEnrollmentController controller) : ActionsBase
{
    /// <summary>
    ///  Change student fromm enrollment
    /// </summary>
    /// <response code="200">Returns the edited resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpPut]
    [Route("students")]
    public async Task<ActionResult> changeStudentEnrollment([FromQuery] string studentId, [FromQuery] string enrollmentId)
    {
        return Ok(await controller.changeStudentEnrollment(studentId, enrollmentId));
    }
}