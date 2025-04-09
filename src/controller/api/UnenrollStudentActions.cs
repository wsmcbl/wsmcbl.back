using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ApiController]
[Route("secretary/enrollments/unenroll")]
public class UnenrollStudentActions(UnenrollStudentController controller) : ActionsBase
{
    /// <summary>Unenroll student in current schoolyear.</summary>
    /// <response code="200">If the resource was updated.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">If the student is not enrolled in the current year.</response>
    [HttpPut]
    [Route("students/{studentId}")]
    [Authorizer("student:enroll")]
    public async Task<IActionResult> unenrollStudent([Required] string studentId)
    {
        await controller.unenrollStudent(studentId);
        return Ok();
    }
}