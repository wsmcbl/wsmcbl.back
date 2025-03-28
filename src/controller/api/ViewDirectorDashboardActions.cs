using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.management;
using wsmcbl.src.middleware;

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
        var studentList = await controller.getStudentRegisterViewListForCurrentSchoolyear();
        var degreeList = await controller.getDegreeListForCurrentSchoolyear();
        
        var result = new DistributionStudentDto(studentList, degreeList);
        return Ok(result);
    }
    
    /// <summary>Get summary of the teachers who entered grades.</summary>
    /// <response code="200">Return the value</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("teachers/grades/summaries")]
    [ResourceAuthorizer("director:summary:read")]
    public async Task<IActionResult> getSummaryTeacherGrades()
    {
        return Ok(await controller.getSummaryTeacherGrades());
    }
}