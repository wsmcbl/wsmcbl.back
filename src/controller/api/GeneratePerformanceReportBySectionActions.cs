using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ApiController]
[Route("academy/teachers/{teacherId}/enrollments/guide")]
[Authorizer("teacher:enrollment:guide")]
public class GeneratePerformanceReportBySectionActions(GeneratePerformanceReportBySectionController controller) : ActionsBase 
{
    /// <summary>Returns enrollment performance by teacher.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Teacher or enrollment not found.</response>
    [HttpGet]
    [Route("performance")]
    public async Task<IActionResult> getPerformanceEnrollmentGuide([Required] string teacherId)
    {
        var result = await controller.getEnrollmentPerformanceByTeacherId(teacherId);
        return Ok(result);
    }
}