using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;

namespace wsmcbl.src.controller.api;

[Route("academy/")]
[ApiController]
public class PrintReportCardByStudentActions(IPrintReportCardByStudentController controller) : BaseController
{
    [HttpGet]
    [Route("students/{studentId}")]
    public async Task<IActionResult> getStudentInformation([Required] string studentId)
    {
        var result = await controller.getStudentInformation(studentId);
        return Ok(result);
    }

    [HttpGet]
    [Route("documents/report-cards/{studentId}")]
    public async Task<IActionResult> getReportCard([Required] string studentId)
    {
        var result = await controller.getReportCard(studentId, "");
        return File(result, "application/pdf", $"{studentId}.report-card.pdf");
    }
}