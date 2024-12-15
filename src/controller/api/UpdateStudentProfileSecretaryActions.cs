using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.accounting;
using wsmcbl.src.dto.secretary;
using wsmcbl.src.exception;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin", "secretary")]
[Route("secretary")]
[ApiController]
public class UpdateStudentProfileSecretaryActions(UpdateStudentProfileController controller) : ActionsBase
{
    /// <summary>
    ///  Returns the basic student list or student full by id.
    /// </summary>
    /// <param name="q">The query string in the format "key:value". Supported keys are "one:{studentId}" and "many:all".</param> 
    /// <response code="200">Returns a resource by query params.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Student not found.</response>
    [ResourceAuthorizer("admin", "secretary", "cashier")]
    [HttpGet]
    [Route("students")]
    public async Task<IActionResult> getStudentById([FromQuery] string q)
    {
        var parts = validateQueryValue(q);
        
        var key = parts[0].ToLower();
        var value = parts[1].ToLower();

        return key switch
        {
            "one" => Ok((await controller.getStudentById(value)).mapToDto()),
            "many" when value.Equals("all") => Ok((await controller.getStudentList()).mapToListBasicDto()),
            _ => BadRequest("Unknown search key.")
        };
    }
    
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
        return Ok(await controller.updateStudent(dto.toEntity()));
    }
    
    /// <summary>Update student profile picture.</summary>
    /// <response code="200">Returns when the resource has been modified.</response>
    /// <response code="400">Parameter is not valid.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource not found.</response>
    [HttpPut]
    [Route("students/{studentId}")]
    public async Task<IActionResult> updateProfilePicture([Required] string studentId, IFormFile profilePicture)
    {
        using var memoryStream = new MemoryStream();
        await profilePicture.CopyToAsync(memoryStream);
        await controller.updateProfilePicture(studentId, memoryStream.ToArray());
        return Ok();
    }
}