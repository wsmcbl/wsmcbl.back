using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.academy;

namespace wsmcbl.src.controller.api;

[Route("academy/")]
[ApiController]
public class PrintReportCardByStudentActions(IPrintReportCardByStudentController controller) : BaseController
{
    [HttpGet]
    [Route("students/{studentId}")]
    public async Task<IActionResult> getStudentInformation([Required] string studentId)
    {
        var student = await controller.getStudentScoreInformation(studentId);
        var teacher = await controller.getTeacherByEnrollment(student.enrollmentId!);

        var result = new StudentScoreInformationDto(student, teacher);
        return Ok(result);
    }

    [HttpGet]
    [Route("documents/report-cards/{studentId}")]
    public async Task<IActionResult> getReportCard([Required] string studentId, [FromBody] PrintReportCardByStudentDto dto)
    {
        var result = await controller.getReportCard(studentId, dto.getTuple());
        return File(result, "application/pdf", $"{studentId}.report-card.pdf");
    }
}