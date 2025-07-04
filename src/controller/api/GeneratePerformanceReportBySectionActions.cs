using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.academy;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ApiController]
[Route("academy/teachers/{teacherId}/enrollments/guide")]
[Authorizer("teacher:enrollment:guide")]
public class GeneratePerformanceReportBySectionActions(GeneratePerformanceReportBySectionController controller) : ActionsBase 
{
    /// <summary>Returns the enrollment performance by teacher for current schoolyear.</summary>
    /// <response code="200">Returns a list.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Teacher or enrollment not found.</response>
    [HttpGet]
    [Route("performance")]
    public async Task<IActionResult> getPerformanceEnrollmentGuide([Required] string teacherId)
    {
        var result = await controller.getStudentListByTeacherId(teacherId);
        return Ok(result.mapListToDto());
    }
    
    /// <summary>Returns grade summary enrollment by teacher.</summary>
    /// <response code="200">Returns a list.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Teacher or enrollment not found.</response>
    [HttpGet]
    [Route("grades/summary")]
    public async Task<IActionResult> getGradeSummaryEnrollmentGuide([Required] string teacherId, [Required] [FromQuery] int partialId)
    {
        var result = await controller.getStudentListByTeacherId(teacherId, partialId);
        return Ok(result.mapListToDto(partialId));
    }
    
    /// <summary>Returns enrollment grade summary by partial in XLSX format.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">If enrollment or partial not found.</response>
    [HttpGet]
    [Route("grades/summary/export")]
    public async Task<IActionResult> getGradeSummaryByEnrollmentId([Required] string teacherId, [Required] [FromQuery] int partialId)
    {
        var result = await controller.getEnrollmentGradeSummary(teacherId, partialId, getAuthenticatedUserId());
        
        return File(result, getContentType(2), $"{teacherId}.grades-summary.xlsx");
    }
}