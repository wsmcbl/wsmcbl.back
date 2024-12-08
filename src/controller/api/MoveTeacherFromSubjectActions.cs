using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.academy;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin","secretary")]
[Route("academy")]
[ApiController]
public class MoveTeacherFromSubjectActions(MoveTeacherFromSubjectController controller) : ControllerBase
{
    /// <summary>
    ///  Returns the list of active teacher
    /// </summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("teachers")]
    public async Task<IActionResult> getTeacherList()
    {
        var list = await controller.getTeacherList();
        return Ok(list.mapListToDto());
    }
    
    /// <summary>
    /// Update the teacher of the enrollment.
    /// </summary>
    /// <response code="200">Returns the edited resource.</response>
    /// <response code="400">If the dto is not valid.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">If the teacher or subject does not exist.</response>
    [HttpPut]
    [Route("teachers")]
    public async Task<IActionResult> updateTeacherEnrollment(MoveTeacherGuideDto dto)
    {
        await controller.updateTeacherEnrollment(dto.newTeacherId);
        return Ok();
    }
}