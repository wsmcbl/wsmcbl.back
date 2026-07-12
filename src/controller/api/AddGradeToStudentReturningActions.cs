using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.academy;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;


[Route("academy")]
[ApiController]
public class AddGradeToStudentReturningActions(AddGradeToStudentReturningController controller) : ActionsBase
{
    /// <summary>Returns partials list by enrollmentId and partialId    .</summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("enrollments/{enrollmentId}/partials/{partialId}/subjects")]
    [Authorizer("admin", "cashier")]
    public async Task<IActionResult> getSubjectForEnrollmentById(string enrollmentId, int partialId)
    {
        var result = await controller.GetSubjectForEnrollmetByPartialId(enrollmentId, partialId);
        return Ok(result);
    }
    
    /// <summary>Saves a list of grades for a student. El label se calculará automáticamente.</summary>
    /// <response code="200">If the grades were successfully recorded.</response>
    /// <response code="400">If the payload validation fails or saving fails.</response>
    /// <response code="401">If the query was made without authentication.</response>
    [HttpPost]
    [Route("grades/student/returning")]
    [Authorizer("admin", "cashier")] 
    public async Task<IActionResult> saveGradesForStudent([FromBody] SaveGradeToStudentReturningDto request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.studentId) || request.grades == null || request.grades.Count == 0)
        {
            return BadRequest("Los datos del estudiante o las notas están vacíos.");
        }
        
        var result = await controller.saveGrades(request);
        
        if (string.IsNullOrWhiteSpace(result.studentId))
        {
            return BadRequest("No se pudieron guardar las notas. Verifica los datos enviados.");
        }
        return Ok(result);
    }
}