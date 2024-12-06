using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.secretary;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin", "secretary")]
[Route("secretary")]
[ApiController]
public class UpdateStudentProfileActions(IUpdateStudentProfileController controller) : ActionsBase
{
    /// <summary>Update student information.</summary>
    /// <response code="200">Returns the modified resource.</response>
    /// <response code="400">Parameter is not valid.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource not found.</response>
    [HttpPut]
    [Route("students")]
    public async Task<IActionResult> updateStudent(StudentFullDto dto)
    {
        await controller.updateStudent(dto.toEntity());
        return Ok();
    }
}