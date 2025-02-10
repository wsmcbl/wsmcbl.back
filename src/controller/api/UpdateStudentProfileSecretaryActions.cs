using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.secretary;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[Route("secretary/students")]
[ApiController]
public class UpdateStudentProfileSecretaryActions(UpdateStudentProfileController controller) : ActionsBase
{
    /// <summary>Returns the basic student list.</summary>
    /// <response code="200">Returns a resource by query params.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("")]
    [ResourceAuthorizer("student:read")]
    public async Task<IActionResult> getStudentList()
    {
        var result = await controller.getStudentList();
        return Ok(result.mapToListBasicDto());
    }
    
    /// <summary>Returns student full by id.</summary> 
    /// <response code="200">Returns a resource by query params.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Student not found.</response>
    [HttpGet]
    [Route("{studentId}")]
    [ResourceAuthorizer("student:read")]
    public async Task<IActionResult> getStudentById([Required] string studentId)
    {
        var result = await controller.getStudentById(studentId);
        return Ok(result.mapToDto());
    }
    
    /// <summary>Update student information.</summary>
    /// <response code="200">Returns the modified resource.</response>
    /// <response code="400">Parameter is not valid.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource not found.</response>
    [HttpPut]
    [Route("{studentId}")]
    [ResourceAuthorizer("student:update")]
    public async Task<IActionResult> updateStudent([Required] string studentId, StudentFullDto dto, [FromQuery] bool withNewToken = false)
    {
        var entity = dto.toEntity();
        entity.studentId = studentId;
        
        return Ok(await controller.updateStudent(entity, withNewToken));
    }
    
    /// <summary>Update student profile picture.</summary>
    /// <response code="200">Returns when the resource has been modified.</response>
    /// <response code="400">Parameter is not valid.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource not found.</response>
    [HttpPut]
    [Route("{studentId}/pictures")]
    [ResourceAuthorizer("student:update")]
    public async Task<IActionResult> updateProfilePicture([Required] string studentId, IFormFile profilePicture)
    {
        using var memoryStream = new MemoryStream();
        await profilePicture.CopyToAsync(memoryStream);
        await controller.updateProfilePicture(studentId, memoryStream.ToArray());
        return Ok();
    }
}