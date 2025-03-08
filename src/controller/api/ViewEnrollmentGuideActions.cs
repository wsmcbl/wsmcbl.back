using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.academy;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[Route("academy/teachers/{teacherId}/enrollments/guide")]
[ApiController]
[ResourceAuthorizer("teacher:enrollment:guide")]
public class ViewEnrollmentGuideActions(ViewEnrollmentGuideController controller) : ActionsBase
{
    /// <summary>Returns enrollment guide by teacher.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Teacher or enrollment not found.</response>
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> getEnrollmentGuide([Required] string teacherId)
    {
        var result = await controller.getEnrollmentGuideByTeacherId(teacherId);
        return Ok(result.mapToDto([]));
    }
    
    /// <summary>Returns enrollment guide metrics by teacher.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Teacher or enrollment not found.</response>
    [HttpGet]
    [Route("metrics")]
    public async Task<IActionResult> getMetricOfEnrollmentGuide([Required] string teacherId)
    {
        var result = await controller.getEnrollmentGuideMetric(teacherId);
        return Ok(result);
    }
}