using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.secretary;
using wsmcbl.src.middleware;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin","secretary")]
[Route("secretary")]
[ApiController]
public class UpdateOfficialEnrollmentActions(UpdateOfficialEnrollmentController controller) : ControllerBase
{
    /// <summary>Update official enrollment resource.</summary>
    /// <response code="200">When update is successful.</response>
    /// <response code="400">The dto in is not valid.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Enrollment or internal record not found.</response>
    [HttpPut]
    [Route("degrees/enrollments/{enrollmentId}")]
    public async Task<IActionResult> updateEnrollment([Required] string enrollmentId, EnrollmentToUpdateDto dto)
    {
        var enrollment = await controller.updateEnrollment(dto.toEntity(enrollmentId));
        return Ok(enrollment.mapToDto());
    }
}