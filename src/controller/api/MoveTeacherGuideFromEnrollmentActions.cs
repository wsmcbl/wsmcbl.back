using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.academy;
using wsmcbl.src.dto.secretary;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin", "secretary")]
[Route("academy")]
[ApiController]
public class MoveTeacherGuideFromEnrollmentActions(MoveTeacherGuideFromEnrollmentController controller) : ControllerBase
{
    /// <summary>Return the list of teacher non-guided.</summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("enrollments/teachers")]
    public async Task<IActionResult> getTeacherList()
    {
        var list = await controller.getTeacherList();
        return Ok(list.mapListToDto());
    }

    /// <summary>Update the teacher guide of the enrollment.</summary>
    /// <param name="enrollmentId">The enrollment id to update.</param>
    /// <param name="teacherId">The id of the new teacher to be assigned.</param>
    /// <response code="200">Returns the edited resource.</response>
    /// <response code="400">If the dto is not valid.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">If the enrollment or teacher does not exist.</response>
    [HttpPut]
    [Route("enrollments/{enrollmentId}")]
    public async Task<IActionResult> setTeacherGuide([Required] string enrollmentId,
        [Required] [FromQuery] string teacherId)
    {
        var enrollment = await controller.getEnrollmentById(enrollmentId);
        var teacher = await controller.getTeacherById(teacherId);
        await controller.assignTeacherGuide(teacher, enrollment);

        return Ok(enrollment.mapToDto(teacher));
    }
}