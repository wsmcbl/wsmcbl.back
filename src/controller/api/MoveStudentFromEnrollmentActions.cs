using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.exception;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[Route("academy")]
[ApiController]
public class MoveStudentFromEnrollmentActions(MoveStudentFromEnrollmentController controller) : ActionsBase
{
    /// <summary>Change student from enrollment.</summary>
    /// <remarks>The student must be enrolled in an existing enrollment.</remarks>
    /// <param name="studentId">The student id must ref to an existing student.</param>
    /// <param name="enrollmentId">The enrollment id must ref to an existing enrollment.</param>
    /// <response code="200">Returns the edited resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource depends on another resource not found (student or enrollment).</response>
    [HttpPut]
    [Route("students")]
    [Authorizer("student:update")]
    public async Task<ActionResult> changeStudentEnrollment([FromQuery] string studentId, [FromQuery] string enrollmentId)
    {
        if (await controller.hasActivePartial())
        {
            throw new ConflictException("This operation cannot be performed. The partial is active.");
        }
        
        var student = await controller.getStudentOrFailed(studentId);
        if (student.enrollmentId == enrollmentId)
        {
            throw new ConflictException("The student is already in this enrollment.");
        }

        var enrollment = await controller.getEnrollmentOrFailed(enrollmentId, student.enrollmentId!);
        
        return Ok(await controller.changeStudentEnrollment(student, enrollment));
    }
}