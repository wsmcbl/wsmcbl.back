using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.academy;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[Authorizer("report:teacher:read")]
[Route("academy/teachers/{teacherId}/subjects")]
[ApiController]
public class ViewTeacherDashboardActions(ViewTeacherDashboardController controller) : ActionsBase
{
    /// <summary>Get summary of the subject for current schoolyear.</summary>
    /// <response code="200">Return the value</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> getSubjectList(string teacherId)
    {
        var result = await controller.getSubjectList(teacherId);
        return Ok(result.mapListToDto());
    }
    
    /// <summary>Get percentage of students evaluated by subjects for current active partial.</summary>
    /// <response code="200">Return the value</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("percentage-evaluated")]
    public async Task<IActionResult> getSummaryPercentageSubjectList(string teacherId)
    {
        var result = await controller.getSubjectListByGrade(teacherId);
        return Ok(result.mapListToSummaryPercentageDto());
    }
}