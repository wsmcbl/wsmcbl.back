using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[Route("academy")]
[ApiController]
public class ViewEnrollmentGuideActions(ViewEnrollmentGuideController controller) : ActionsBase
{
    /// <summary>Returns the list of active enrollment by teacher.</summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Teacher not found.</response>
    [HttpGet]
    [Route("teachers/{teacherId}/enrollments/temporal")]
    [ResourceAuthorizer("teacher:read")]
    public async Task<IActionResult> getEnrollmentGuide([Required] string teacherId)
    {
        var result = await controller.getEnrollmentGuideByTeacherId(teacherId);
        return Ok(result);
    }
}