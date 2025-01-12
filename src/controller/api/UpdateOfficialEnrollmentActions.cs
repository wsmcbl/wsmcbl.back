using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.academy;
using wsmcbl.src.dto.secretary;
using wsmcbl.src.exception;
using wsmcbl.src.middleware;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin", "secretary")]
[Route("academy")]
[ApiController]
public class UpdateOfficialEnrollmentActions : ControllerBase
{
    private readonly UpdateOfficialEnrollmentController enrollmentController;
    private readonly MoveTeacherGuideFromEnrollmentController teacherGuideController;
    private readonly MoveTeacherFromSubjectController subjectController;

    public UpdateOfficialEnrollmentActions(UpdateOfficialEnrollmentController enrollmentController,
        MoveTeacherGuideFromEnrollmentController teacherGuideController,
        MoveTeacherFromSubjectController subjectController)
    {
        this.enrollmentController = enrollmentController;
        this.teacherGuideController = teacherGuideController;
        this.subjectController = subjectController;
    }
    
    /// <summary>Returns the list of teacher.</summary>
    /// <param name="q">
    /// Supported values are "active" and "non-guided".
    /// </param>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="400">If the query parameter is missing or not in the correct format.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("teachers")]
    public async Task<IActionResult> getTeacherList([Required] [FromQuery] string q)
    {
        List<TeacherEntity> list;
        switch (q.ToLower())
        {
            case "active":
            {
                list = await subjectController.getTeacherList();
                break;
            }
            case "non-guided":
            {
                list = await teacherGuideController.getTeacherList();
                break;
            }
            default:
                return NotFound("Unknown type value.");
        }

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
    [Route("enrollments/{enrollmentId}/teachers")]
    public async Task<IActionResult> setTeacherGuide([Required] string enrollmentId,
        [Required] [FromQuery] string teacherId)
    {
        var enrollment = await teacherGuideController.getEnrollmentById(enrollmentId);
        var teacher = await teacherGuideController.getTeacherById(teacherId);
        await teacherGuideController.assignTeacherGuide(teacher, enrollment);

        return Ok(enrollment.mapToDto(teacher));
    }

    /// <summary>Update the teacher of the subject.</summary>
    /// <param name="enrollmentId">The enrollment id to update.</param>
    /// <param name="subjectId">The subject id to update.</param>
    /// <param name="teacherId">The teacher id to assign.</param>
    /// <response code="200">Returns the edited resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">If the teacher or subject does not exist.</response>
    [HttpPut]
    [Route("enrollments/{enrollmentId}/subjects/{subjectId}")]
    public async Task<IActionResult> updateTeacherFromSubject([Required] string enrollmentId,
        [Required] string subjectId, [Required] [FromQuery] string teacherId)
    {
        if (await subjectController.isThereAnActivePartial())
        {
            throw new ConflictException("This operation cannot be performed. The partial is active.");
        }

        var teacher = await subjectController.getTeacherById(teacherId);
        if (teacher == null)
        {
            throw new EntityNotFoundException("Teacher", teacherId);
        }

        await subjectController.updateTeacherFromSubject(subjectId, enrollmentId, teacherId);
        return Ok();
    }

    /// <summary>Update official enrollment resource.</summary>
    /// <response code="200">When update is successful.</response>
    /// <response code="400">The dto in is not valid.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Enrollment or internal record not found.</response>
    [HttpPut]
    [Route("enrollments/{enrollmentId}")]
    public async Task<IActionResult> updateEnrollment([Required] string enrollmentId, EnrollmentToUpdateDto dto)
    {
        await enrollmentController.updateEnrollment(dto.toEntity(enrollmentId));
        return Ok();
    }
    
    /// <summary>Get enrollment resource.</summary>
    /// <response code="200">If the request is successful.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Enrollment or internal record not found.</response>
    [HttpGet]
    [Route("enrollments/{enrollmentId}")]
    public async Task<IActionResult> getEnrollmentById([Required] string enrollmentId)
    {
        var enrollment = await enrollmentController.getEnrollmentById(enrollmentId);
        return Ok(enrollment.mapToDto());
    }
}