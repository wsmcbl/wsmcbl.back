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
        return Ok(await controller.getEnrollmentListByTeacherId(teacherId));
    }
    
    /// <summary>
    ///  Returns the list of subject corresponding to a teacher and enrollment
    /// </summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="404">Teacher or enrollment not found.</response>
    [HttpGet]
    [Route("enrollments/subjects")]
    public async Task<IActionResult> getSubjectList(SubjectDto dto) // teacherid, enrollmentid
    {
        var teacherId = ";";
        var enrollmentId = ";";
        return Ok(await controller.getSubjectList(teacherId, enrollmentId));
    }
    
    /// <summary>
    ///  Update the grades of subject corresponding to a teacher and enrollment
    /// </summary>
    /// <response code="200">When update is successful.</response>
    /// <response code="400">The dto in is not valid.</response>
    /// <response code="404">Teacher or .. not found.</response>
    [HttpPut]
    [Route("enrollments/subjects")]
    public async Task<IActionResult> addGrades(SubjectDto dto) // teacherid, enrollmentid, grades
    {
        var teacherId = ";";
        var grades = new List<string>();
        await controller.addGrades(teacherId, grades);
        return Ok();
    }
}