using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.exception;

namespace wsmcbl.src.controller.api;

[Route("academy")]
[ApiController]
public class ViewGradeOnlineActions(ViewGradeOnlineController controller) : ControllerBase
{
    /// <summary>
    ///  Return the grade report by student.
    /// </summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Student not found.</response>
    /// <response code="409">Student has no solvency.</response>
    [HttpGet]
    [Route("grades/{studentId}")]
    public async Task<IActionResult> getGradesReport(string studentId, [FromQuery] string token)
    {
        if (!await controller.isTheStudentSolvent(studentId))
        {
            throw new ConflictException($"Student with id ({studentId}) has no solvency.");
        }

        if (!await controller.isTokenCorrect(studentId, token))
        {
            throw new UnauthorizedException("Student unauthorized.");
        }
        
        var result = await controller.getGradeReport(studentId);
        return File(result, "application/pdf", $"{studentId}.grade-report.pdf");
    }
}