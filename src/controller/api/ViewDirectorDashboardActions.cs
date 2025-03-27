using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;

namespace wsmcbl.src.controller.api;

[Route("management")]
[ApiController]
public class ViewDirectorDashboardActions(ViewDirectorDashboardController controller) : ActionsBase
{
    /// <summary>Get summary of the students for current schoolyear.</summary>
    /// <response code="200">Return the value</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("students/summaries")]
    public async Task<IActionResult> getSummaryStudentQuantity()
    {
        return Ok(await controller.getSummaryStudentQuantity());
    }
    
    /// <summary>Get summary of the teachers who entered grades.</summary>
    /// <response code="200">Return the value</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("teachers/grades/summaries")]
    public async Task<IActionResult> getSummaryTeacherGrades()
    {
        return Ok(await controller.getSummaryTeacherGrades());
    }
}