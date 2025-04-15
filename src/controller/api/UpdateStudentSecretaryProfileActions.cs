using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.secretary;
using wsmcbl.src.middleware;
using wsmcbl.src.model;

namespace wsmcbl.src.controller.api;

[Route("secretary/students")]
[ApiController]
public class UpdateStudentSecretaryProfileActions(UpdateStudentProfileController controller) : ActionsBase
{
    /// <summary>Returns paged basic student list.</summary>
    /// <remarks>Values for sortBy: studentId, fullName, isActive, tutor, schoolyear and enrollment.</remarks>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("")]
    [Authorizer("student:read")]
    public async Task<IActionResult> getPaginatedStudentView([FromQuery] StudentPagedRequest request)
    {
        request.checkSortByValue(["studentId", "fullName", "isActive", "tutor", "schoolyear", "enrollment"]);
        
        var result = await controller.getPaginatedStudentView(request);
        
        var pagedResult = new PagedResult<BasicStudentDto>(result.data.mapToListBasicDto());
        pagedResult.setup(result);
        
        return Ok(pagedResult);
    }
    
    /// <summary>Returns student full by id.</summary> 
    /// <response code="200">Returns a resource by query params.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Student not found.</response>
    [HttpGet]
    [Route("{studentId}")]
    [Authorizer("student:read")]
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
    [Authorizer("student:update")]
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
    [Authorizer("student:update")]
    public async Task<IActionResult> updateProfilePicture([Required] string studentId, IFormFile profilePicture)
    {
        using var memoryStream = new MemoryStream();
        await profilePicture.CopyToAsync(memoryStream);
        await controller.updateProfilePicture(studentId, memoryStream.ToArray());
        return Ok();
    }
    
    /// <summary>Change student state (active or inactive).</summary>
    /// <response code="200">Returns when the resource has been modified.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource not found.</response>
    [HttpPut]
    [Route("{studentId}/state")]
    [Authorizer("student:update")]
    public async Task<IActionResult> updateProfileState([Required] string studentId)
    {
        await controller.changeProfileState(studentId);
        return Ok();
    }
    
    /// <summary>Change student access token.</summary>
    /// <response code="200">Returns when the resource has been modified.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource not found.</response>
    [HttpPut]
    [Route("{studentId}/token")]
    [Authorizer("student:update")]
    public async Task<IActionResult> updateProfileToken([Required] string studentId)
    {
        var student = await controller.changeProfileToken(studentId);
        return Ok();
    }
}