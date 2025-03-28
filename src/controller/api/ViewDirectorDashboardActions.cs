using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.management;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[Route("management")]
[ApiController]
public class ViewDirectorDashboardActions(ViewDirectorDashboardController controller) : ActionsBase
{
    /// <summary>Get summary of the revenue for current month.</summary>
    /// <response code="200">Return the value</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("revenues/")]
    [ResourceAuthorizer("report:principal:read")]
    public async Task<IActionResult> getSummaryRevenue()
    {
        await controller.getSummaryRevenue();
        return Ok(new
        {
            expectedIncomeThisMonth = 88500,
            expectedIncomeReceived = 50100,
            totalIncomeThisMonth = 100000
        });
    }
    
    /// <summary>Get last ten incidents.</summary>
    /// <response code="200">Return the value</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("incidents/")]
    [ResourceAuthorizer("report:principal:read")]
    public async Task<IActionResult> getLastIncidents()
    {
        return Ok(await controller.getLastIncidents());
    }
    
    /// <summary>Get summary of the students for current schoolyear.</summary>
    /// <response code="200">Return the value</response>
    [HttpGet]
    [Route("students/distributions")]
    public async Task<IActionResult> getStudentDistribution()
    {
        var studentList = await controller.getStudentRegisterViewListForCurrentSchoolyear();
        var degreeList = await controller.getDegreeListForCurrentSchoolyear();
        
        return Ok(new DistributionStudentDto(studentList, degreeList));
    }
    
    /// <summary>Get summary of the teachers who entered grades.</summary>
    /// <response code="200">Return the value</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("teachers/grades/summaries")]
    [ResourceAuthorizer("report:principal:read")]
    public async Task<IActionResult> getSummaryTeacherGrades()
    {
        var result = await controller.getSummaryTeacherGrades();
        return Ok(result.mapListToDto());
    }
    
    /// <summary>Returns subject list for current schoolyear.</summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("subjects")]
    [ResourceAuthorizer("report:principal:read")]
    public async Task<IActionResult> getSubjectList()
    {
        var result = await controller.getSubjectList();
        return Ok(result);
    }
}