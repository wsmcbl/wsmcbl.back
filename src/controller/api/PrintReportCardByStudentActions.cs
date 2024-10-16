using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.academy;
using wsmcbl.src.exception;

namespace wsmcbl.src.controller.api;

[Route("academy/")]
[ApiController]
public class PrintReportCardByStudentActions(IPrintReportCardByStudentController controller) : ControllerBase
{
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