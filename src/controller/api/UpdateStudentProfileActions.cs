using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.accounting;
using wsmcbl.src.dto.secretary;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin", "secretary")]
[ApiController]
public class UpdateStudentProfileActions(UpdateStudentProfileController controller) : ActionsBase
{
    /// <summary>Update student information.</summary>
    /// <response code="200">Returns the modified resource.</response>
    /// <response code="400">Parameter is not valid.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource not found.</response>
    [HttpPut]
    [Route("secretary/students")]
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
    [Route("secretary/students/{studentId}")]
    public async Task<IActionResult> updateProfilePicture([Required] string studentId, IFormFile profilePicture)
    {
        using var memoryStream = new MemoryStream();
        await profilePicture.CopyToAsync(memoryStream);
        await controller.updateProfilePicture(studentId, memoryStream.ToArray());
        return Ok();
    }
    
    /// <summary>Update student discount.</summary>
    /// <response code="200">If the resource was edited correctly.</response>
    /// <response code="400">Parameter is not valid.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource not found.</response>
    [ResourceAuthorizer("cashier")]
    [HttpPut]
    [Route("accounting/students")]
    public async Task<IActionResult> updateDiscount(ChangeStudentDiscountDto dto)
    {
        await controller.updateStudentDiscount(dto.studentId, dto.discountId);
        return Ok();
    }
}