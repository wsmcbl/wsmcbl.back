using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.academy;
using wsmcbl.src.exception;
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
    /// Update the teacher of the subject.
    /// </summary>
    /// <param name="subjectId">The subject id.</param>
    /// <param name="teacherId">The teacher id.</param>
    /// <response code="200">Returns the edited resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">If the teacher or subject does not exist.</response>
    [HttpPut]
    [Route("subjects")]
    public async Task<IActionResult> updateTeacherEnrollment([FromQuery] string subjectId, [FromQuery] string teacherId)
    {
        if (await controller.isThereAnActivePartial())
        {
            throw new ConflictException("This operation cannot be performed. The partial is active.");
        }
        
        var teacher = await controller.getTeacherById(teacherId);
        if (teacher == null)
        {
            throw new EntityNotFoundException("Teacher", teacherId);
        }
        
        await controller.updateTeacherFromSubject(subjectId, teacherId);
        return Ok();
    }
}