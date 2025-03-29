using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.exception;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[Route("academy")]
[ApiController]
public class PrintReportCardByStudentActions(PrintReportCardByStudentController controller) : ActionsBase
{
    /// <summary>Returns the report-card by student.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="500">Error creating document.</response>
    [HttpGet]
    [Route("students/{studentId}/report-card/export")]
    [Authorizer("student:read")]
    public async Task<IActionResult> getReportCard([Required] string studentId)
    {
        var isSolvency = await controller.isStudentSolvent(studentId);
        if (!isSolvency)
        {
            throw new ConflictException($"Student with id ({studentId}) has no solvency.");
        }
        
        var result = await controller.getReportCard(studentId, getAuthenticatedUserId());
        return File(result, "application/pdf", $"{studentId}.report-card.pdf");
    }
}