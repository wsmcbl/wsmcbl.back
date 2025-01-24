using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.academy;
using wsmcbl.src.middleware;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin", "teacher")]
[Route("academy")]
[ApiController]
public class AddingStudentGradesActions(AddingStudentGradesController controller) : ControllerBase
{
    /// <summary>Returns the list of partials.</summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("partials")]
    public async Task<IActionResult> getPartialList()
    {
        var result = await controller.getPartialList();
        return Ok(result.mapListToDto());
    }

    /// <summary>Returns teacher information by id.</summary>
    /// <response code="200">Returns the resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Teacher not found.</response>
    [HttpGet]
    [Route("teachers/{teacherId}")]
    public async Task<IActionResult> getTeacherById([Required] string teacherId)
    {
        var result = await controller.getTeacherById();
        return Ok(result.mapToBasicDto());
    }

    /// <summary>Returns the list of active enrollment by teacher id.</summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Teacher not found.</response>
    [HttpGet]
    [Route("teachers/{teacherId}/enrollments")]
    public async Task<IActionResult> getEnrollmentListByTeacherId([Required] string teacherId)
    {
        var result = await controller.getEnrollmentListByTeacherId(teacherId);
        return Ok(result.mapListToDto());
    }

    /// <summary>
    ///  Returns the list of subject, students and grades corresponding to a teacher grade's information
    /// </summary>
    /// <response code="200">Returns the list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Teacher, enrollment or partial not found.</response>
    [HttpGet]
    [Route("teachers/{teacherId}/enrollments/{enrollmentId}")]
    public async Task<IActionResult> getEnrollmentToAddGrades([Required] string teacherId,
        [Required] string enrollmentId, [Required] [FromQuery] int partial)
    {
        var enrollment = await controller.getEnrollmentById(enrollmentId);

        var subjectPartial = new SubjectPartialEntity(teacherId, enrollmentId, partial);
        var subjectPartialList = await controller.getSubjectPartialList(subjectPartial);

        return Ok(new EnrollmentToAddGradesDto(enrollment, subjectPartialList));
    }

    /// <summary>
    ///  Update the grades of subject corresponding to a teacher grade's information
    /// </summary>
    /// <response code="200">When update is successful.</response>
    /// <response code="400">The dto in is not valid.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Teacher or internal record not found.</response>
    [HttpPut]
    [Route("teachers/{teacherId}/enrollments/{enrollmentId}")]
    public async Task<IActionResult> addGrades([Required] string teacherId,
        [Required] string enrollmentId, [Required] [FromQuery] int partial, List<GradeDto> gradeList)
    {
        var subjectPartial = new SubjectPartialEntity(teacherId, enrollmentId, partial);
        await controller.addGrades(subjectPartial, gradeList.toEntity());
        return Ok();
    }
}