using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.management;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[Route("management")]
[ApiController]
public class ViewPrincipalDashboardActions(ViewPrincipalDashboardController controller) : ActionsBase
{
    /// <summary>Get summary of the revenue for current month.</summary>
    /// <response code="200">Return the value</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("revenues/")]
    [Authorizer("report:principal:read")]
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
    [Authorizer("report:principal:read")]
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
    [Authorizer("report:principal:read")]
    public async Task<IActionResult> getSummaryTeacherGrades()
    {
        var result = await controller.getSummaryTeacherGrades();
        return Ok(result.mapListToDto());
    }
    
    /// <summary>Returns subject name list for current schoolyear.</summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("subjects")]
    [Authorizer("report:principal:read")]
    public async Task<IActionResult> getSubjectList()
    {
        var subjectList = await controller.getSubjectList();
        var degreeList = await controller.getDegreeList();
        
        return Ok(subjectList.mapListToDto(degreeList));
    }
    
    
    /// <summary>Returns enrollment grade summary by partial in XLSX format.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">If enrollment or partial not found.</response>
    [HttpGet]
    [Route("enrollments/{enrollmentId}/grades/export")]
    [Authorizer("report:principal:read")]
    public async Task<IActionResult> getGradeSummaryByEnrollmentId([Required] string enrollmentId, [Required] [FromQuery] int partial)
    {
        var result = await controller.getGradeSummaryByEnrollmentId(enrollmentId, partial, getAuthenticatedUserId());
        
        return File(result, getContentType(2), $"{enrollmentId}.grades-summary.xlsx");
    }
}