using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.academy;
using wsmcbl.src.exception;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin","secretary")]
[Route("academy/")]
[ApiController]
public class PrintReportCardByStudentActions(PrintReportCardByStudentController controller) : ControllerBase
{
    /// <summary>
    ///  Returns the student by id.
    /// </summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Student not found.</response>
    [HttpGet]
    [Route("students/{studentId}")]
    public async Task<IActionResult> getStudentInformation([Required] string studentId)
    {
        var student = await controller.getStudentGradesInformation(studentId);
        var isSolvency = await controller.getStudentSolvency(studentId);
        var teacher = await controller.getTeacherByEnrollment(student.enrollmentId!);

        var result = new StudentScoreInformationDto(student, teacher);
        result.setSolvencyStateMessage(isSolvency);
        return Ok(result);
    }
    
    /// <summary>
    ///  Returns the report-card by student
    /// </summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="500">Error creating document.</response>
    [HttpGet]
    [Route("documents/report-cards/{studentId}")]
    public async Task<IActionResult> getReportCard([Required] string studentId)
    {
        var isSolvency = await controller.getStudentSolvency(studentId);

        if (!isSolvency)
        {
            throw new BadRequestException($"Student with id ({studentId}) is not solvency.");
        }
        
        var result = await controller.getReportCard(studentId);
        return File(result, "application/pdf", $"{studentId}.report-card.pdf");
    }
}