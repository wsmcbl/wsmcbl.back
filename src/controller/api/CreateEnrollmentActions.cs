using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.secretary;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin","secretary")]
[Route("secretary/degrees")]
[ApiController]
public class CreateEnrollmentActions(CreateEnrollmentController controller) : ActionsBase
{
    /// <summary>Returns the list of active degrees.</summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [ResourceAuthorizer("teacher")]
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> getDegreeList()
    {
        var result = await controller.getDegreeList();
        return Ok(result.mapListToBasicDto());
    }

    /// <summary>Create new enrollment.</summary>
    /// <param name="degreeId">The degreeId must be not empty.</param>
    /// <param name="quantity">The quantity value must be between 1 and 7.</param>
    /// <response code="201">If the resource is created.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource depends on another resource not found (degree) or quantity invalid.</response>
    [HttpPost]
    [Route("{degreeId}/enrollments")]
    public async Task<IActionResult> createEnrollment([Required] string degreeId, [Required] [FromQuery] int quantity)
    {
        var degree = await controller.createEnrollments(degreeId, quantity);
        var result = new DegreeForCreateEnrollmentDto(degree);
        
        return CreatedAtAction(null, new { degree.degreeId }, result);
    }
    
    /// <summary>Initialize enrollment record.</summary>
    /// <remarks>The "label" value is not required.</remarks>
    /// <response code="200">When init is successful.</response>
    /// <response code="400">The dto is not valid.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Enrollment not found.</response>
    [HttpPut]
    [Route("enrollments")]
    public async Task<IActionResult> updateEnrollment(EnrollmentToCreateDto dto)
    {
        var enrollment = await controller.updateEnrollment(dto.toEntity());

        return Ok(new EnrollmentToCreateDto(enrollment));
    }    
}