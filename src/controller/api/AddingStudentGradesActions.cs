using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.academy;
using wsmcbl.src.exception;
using wsmcbl.src.middleware;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.controller.api;

[Route("academy")]
[ApiController]
public class AddingStudentGradesActions(AddingStudentGradesController controller) : ActionsBase
{
    /// <summary>Returns partials list.</summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("partials")]
    [Authorizer("partial:read")]
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
    [Authorizer("teacher:read")]
    public async Task<IActionResult> getTeacherById([Required] string teacherId)
    {
        var result = await controller.getTeacherById(teacherId);
        return Ok(result.mapToDto());
    }

    /// <summary>Returns enrollment active list by teacher.</summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Teacher not found.</response>
    [HttpGet]
    [Route("teachers/{teacherId}/enrollments")]
    [Authorizer("teacher:read")]
    public async Task<IActionResult> getEnrollmentListByTeacherId([Required] string teacherId)
    {
        var result = await controller.getEnrollmentListByTeacherId(teacherId);
        return Ok(result.mapListToDto(teacherId));
    }

    /// <summary>Returns subject grades and students for enrollment by partial.</summary>
    /// <response code="200">Returns the list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Teacher, enrollment or partial not found.</response>
    /// <response code="409">If there is not a grade records.</response>
    [HttpGet]
    [Route("teachers/{teacherId}/enrollments/{enrollmentId}")]
    [Authorizer("teacher:read")]
    public async Task<IActionResult> getEnrollmentToAddGrades([Required] string teacherId, [Required] string enrollmentId, 
        [Required] [FromQuery] int partialId)
    {
        var enrollment = await controller.getEnrollmentById(enrollmentId);

        var subjectPartial = new SubjectPartialEntity(teacherId, enrollmentId, partialId);
        var subjectPartialList = await controller.getSubjectPartialList(subjectPartial);

        return Ok(new EnrollmentToAddGradesDto(enrollment, subjectPartialList));
    }

    /// <summary>Update subject grades for enrollment by partial.</summary>
    /// <response code="200">When update is successful.</response>
    /// <response code="400">The dto in is not valid.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Teacher or internal record not found.</response>
    /// <response code="409">If the grade record is not active.</response>
    [HttpPut]
    [Route("teachers/{teacherId}/enrollments/{enrollmentId}")]
    [Authorizer("grade:update")]
    public async Task<IActionResult> addGrades([Required] string teacherId,
        [Required] string enrollmentId, [Required] [FromQuery] int partialId, List<GradeDto> gradeList)
    {
        if (await controller.recordIsNotActive(partialId))
        {
            throw new ConflictException("The grade record is not active.");
        }
        
        var subjectPartial = new SubjectPartialEntity(teacherId, enrollmentId, partialId);
        await controller.addGrades(subjectPartial, gradeList.toEntity());
        return Ok();
    }
    
    /// <summary>Returns subject grades and students for enrollment by partial in XLSX format.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Teacher, enrollment or partial not found.</response>
    /// <response code="409">If there is not a grade records.</response>
    [HttpGet]
    [Route("teachers/{teacherId}/enrollments/{enrollmentId}/export")]
    [Authorizer("teacher:read")]
    public async Task<IActionResult> getEnrollmentToAddGradesDocument([Required] string teacherId, [Required] string enrollmentId, 
        [Required] [FromQuery] int partialId)
    {
        var subjectPartial = new SubjectPartialEntity(teacherId, enrollmentId, partialId);
        var result = await controller.getEnrollmentToAddGradesDocument(subjectPartial, getAuthenticatedUserId());
        return File(result, getContentType(2), $"{teacherId}.{enrollmentId}.grades.xlsx");
    }
}