using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[Route("academy")]
[ApiController]
public class ViewTeacherDashboardActions(ViewTeacherDashboardController controller) : ActionsBase
{
    /// <summary>Get summary of the subject for current schoolyear.</summary>
    /// <response code="200">Return the value</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("teachers/{teacherId}/subjects")]
    [Authorizer("report:teacher:read")]
    public async Task<IActionResult> getSubjectList(string teacherId)
    {
        var result = await controller.getSubjectList(teacherId);
        return Ok(result);
    }
    
    /// 
    /// <summary>Get percentage of students evaluated by subjects.</summary>
    /// <response code="200">Return the value</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("teachers/{teacherId}/subjects/summary")]
    [Authorizer("report:teacher:read")]
    public async Task<IActionResult> getSummaryPercentageSubjectList(string teacherId)
    {
        var result = await controller.getSubjectListByGrade(teacherId);
        return Ok(result);
    }
}