using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.secretary;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("secretary")]
[Route("academy")]
[ApiController]
public class CreateEnrollmentActions(CreateEnrollmentController controller) : ActionsBase
{
    /// <summary>
    ///  Update the enrollment resource
    /// </summary>
    /// <response code="200">When update is successful.</response>
    /// <response code="400">The dto in is not valid.</response>
    /// <response code="404">Enrollment or internal record not found.</response>
    [HttpPut]
    [Route("degrees/enrollments")]
    public async Task<IActionResult> updateEnrollment(EnrollmentToUpdateDto dto)
    {
        var enrollment = await controller.updateEnrollment(dto.toEntity());

        return Ok(enrollment);
    }    
}