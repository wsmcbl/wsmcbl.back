using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.academy;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ApiController]
[Route("academy/teachers/{teacherId}/enrollments/guide/stats/evaluated")]
//[Authorizer("teacher:enrollment:guide")]
public class GenerateEvaluationStatsBySectionActions(GenerateEvaluationStatsBySectionController controller) : ActionsBase
{
    /// <summary>Returns evaluated summary stats enrollment by teacher.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Teacher or enrollment not found.</response>
    [HttpGet]
    [Route("summary")]
    public async Task<IActionResult> getEvaluationStats([Required] string teacherId, [Required] [FromQuery] int partial)
    {
        var result = await controller.getStudentListByTeacherId(teacherId, partial);
        return Ok(new EvaluatedSummaryDto(result, result));
    }
    
    /// <summary>Returns subject evaluated stats enrollment by teacher.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Teacher or enrollment not found.</response>
    [HttpGet]
    [Route("subjects")]
    public async Task<IActionResult> getSubjectStats([Required] string teacherId, [Required] [FromQuery] int partial)
    {
        var result = await controller.getStudentListByTeacherId(teacherId, partial);
        return Ok(new EvaluatedSummaryDto(result, result));
    }
    
    /// <summary>Returns distribution evaluated stats enrollment by teacher.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Teacher or enrollment not found.</response>
    [HttpGet]
    [Route("distribution")]
    public async Task<IActionResult> getDistributionStats([Required] string teacherId, [Required] [FromQuery] int partial)
    {
        var result = await controller.getStudentListByTeacherId(teacherId, partial);
        return Ok(new DistributionSummaryDto(result, result));
    }
}