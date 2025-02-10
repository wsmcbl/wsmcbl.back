using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.secretary;
using wsmcbl.src.exception;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[Route("secretary/students/levels")]
[ApiController]
public class CorrectEducationalLevelActions(CorrectEducationalLevelController controller) : ActionsBase
{
    /// <summary>Get educational level information of the student.</summary>
    /// <response code="200">Returns the resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">If resource not exist.</response>
    [HttpGet]
    [Route("")]
    [ResourceAuthorizer("student:read")]
    public async Task<IActionResult> getStudentInformationLevelById(string studentId)
    {
        var student = await controller.getStudentById(studentId);
        return Ok(new StudentWithLevelDto(student));
    }
    
    /// <summary>Change educational level of the student.</summary>
    /// <remarks>The level value must be 1, 2 or 3.</remarks>
    /// <response code="200">Returns the edited resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">If a resource not exist (student or tariff).</response>
    /// <response code="409">If the student has the same level.</response>
    [HttpPut]
    [Route("")]
    [ResourceAuthorizer("student:update")]
    public async Task<IActionResult> moveFromEducationLevel([FromQuery] string studentId, [FromQuery] int level)
    {
        if (level is < 1 or > 3)
        {
            throw new BadRequestException("The level value must be 1, 2 o 3.");
        }
        
        var student = await controller.changeEducationalLevel(studentId, level);
        return Ok(new StudentWithLevelDto(student));
    }
}