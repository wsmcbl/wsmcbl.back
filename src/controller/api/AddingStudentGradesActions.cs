using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.secretary;

namespace wsmcbl.src.controller.api;

[Route("academy")]
[ApiController]
public class AddingStudentGradesActions(IAddingStudentGradesController controller) : ControllerBase
{
    /// <summary>
    ///  Returns the list of active enrollment by teacher id.
    /// </summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="404">Teacher not found.</response>
    [HttpGet]
    [Route("enrollments/{teacherId}")]
    public async Task<IActionResult> getEnrollmentListByTeacherId([Required] string teacherId)
    {
        var result = await controller.getEnrollmentListByTeacherId(teacherId);
        return Ok(result.mapListToDto());
    }
    
    /// <summary>
    ///  Returns the list of partials.
    /// </summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    [HttpGet]
    [Route("partials}")]
    public async Task<IActionResult> getPartialList()
    {
        var result = await controller.getPartialList();
        return Ok(result.mapListToDto());
    }
    
    /// <summary>
    ///  Returns the list of subject corresponding to a teacher and enrollment
    /// </summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="404">Teacher or enrollment not found.</response>
    [HttpGet]
    [Route("enrollments/subjects")]
    public async Task<IActionResult> getEnrollmentSubjects(TeacherEnrollmentDto dto) // teacherid, enrollmentid
    {
        var result = await controller.getEnrollmentByTeacher(dto.teacherId, dto.enrollmentId);
        return Ok(result.mapListToDto());
    }
    
    /// <summary>
    ///  Update the grades of subject corresponding to a teacher and enrollment
    /// </summary>
    /// <response code="200">When update is successful.</response>
    /// <response code="400">The dto in is not valid.</response>
    /// <response code="404">Teacher or .. not found.</response>
    [HttpPut]
    [Route("enrollments/subjects")]
    public async Task<IActionResult> addGrades(SubjectParialDto dto) // teacherid, enrollmentid, grades
    {
        await controller.addGrades(dto.teacherId, dto.grades);
        return Ok();
    }
}