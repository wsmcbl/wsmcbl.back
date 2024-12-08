using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.academy;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin","secretary")]
[Route("academy")]
[ApiController]
public class MoveTeacherFromSubjectActions(MoveTeacherFromSubjectController fromSubjectController) : ControllerBase
{
    /// <summary>
    /// Update the teacher of the enrollment.
    /// </summary>
    /// <response code="200">Returns the edited resource.</response>
    /// <response code="400">If the dto is not valid.</response>
    /// <response code="404">If the enrollment or teacher does not exist.</response>
    [HttpPut]
    [Route("teachers")]
    public async Task<IActionResult> updateTeacherEnrollment(MoveTeacherGuideDto dto)
    {
        await fromSubjectController.updateTeacherEnrollment(dto.newTeacherId);
        return Ok();
    }
}