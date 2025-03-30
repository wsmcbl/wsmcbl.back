using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.academy;
using wsmcbl.src.exception;

namespace wsmcbl.src.controller.api;

[Route("academy/students/{studentId}")]
[ApiController]
public class ViewGradeOnlineActions(ViewGradeOnlineController controller) : ControllerBase
{
    /// <summary>Returns the student by id.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="404">Student not found.</response>
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> getStudentInformation([Required] string studentId)
    {
        var student = await controller.getStudent(studentId);
        var isSolvency = await controller.isStudentSolvent(studentId);
        var teacher = await controller.getTeacherByEnrollment(student.enrollmentId!);

        var result = new StudentScoreInformationDto(student, teacher);
        result.setSolvency(isSolvency);
        return Ok(result);
    }
    
    /// <summary>Return grade report by student id in PDF format.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the student unauthorized.</response>
    /// <response code="404">Student not found.</response>
    /// <response code="409">Student has no solvency.</response>
    [HttpGet]
    [Route("grades/export")]
    public async Task<IActionResult> getGradesReport(string studentId, [FromQuery] string token)
    {
        if (!await controller.isTokenValid(studentId, token))
        {
            throw new UnauthorizedException("Student unauthorized.");
        }
        
        if (!await controller.isStudentSolvent(studentId))
        {
            throw new ConflictException($"Student with id ({studentId}) has no solvency.");
        }
        
        var result = await controller.getGradeReport(studentId);
        return File(result, "application/pdf", $"{studentId}.grade-report.pdf");
    }
}