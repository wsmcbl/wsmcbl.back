using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ApiController]
[Route("academy/teachers/{teacherId}/enrollments/guide")]
[Authorizer("teacher:enrollment:guide")]
public class GenerateEvaluationStatsBySectionActions(GenerateEvaluationStatsBySectionController controller) : ActionsBase
{
    /// <summary>Returns evaluation stats enrollment by teacher.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Teacher or enrollment not found.</response>
    [HttpGet]
    [Route("stats")]
    public async Task<IActionResult> getEvaluationStats([Required] string teacherId, [Required] [FromQuery] int partial)
    {
        var result = await controller.getEvaluationStatsByTeacherId(teacherId, partial);
        return Ok(result);
    }
}