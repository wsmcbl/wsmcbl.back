using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.academy;
using wsmcbl.src.dto.secretary;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin","secretary")]
[Route("academy")]
[ApiController]
public class MoveTeacherGuideFromEnrollmentActions(MoveTeacherGuideFromEnrollmentController controller) : ControllerBase
{
    /// <summary>
    /// Return the list of teacher non-guided.
    /// </summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    [HttpGet]
    [Route("enrollments/teachers")]
    public async Task<IActionResult> getTeacherList()
    {
        var list = await controller.getTeacherList();
        return Ok(list.mapListToDto());
    }
    
    /// <summary>
    /// Update the teacher guide of the enrollment.
    /// </summary>
    /// <response code="200">Returns the edited resource.</response>
    /// <response code="400">If the dto is not valid.</response>
    /// <response code="404">If the enrollment or teacher does not exist.</response>
    [HttpPut]
    [Route("enrollments")]
    public async Task<IActionResult> setTeacherGuide(MoveTeacherGuideDto dto)
    {
        var enrollment = await controller.getEnrollmentById(dto.enrollmentId);
        var teacher = await controller.getTeacherById(dto.newTeacherId);
        await controller.assignTeacherGuide(teacher, enrollment);
        
        return Ok(enrollment.mapToDto(teacher));
    }
}