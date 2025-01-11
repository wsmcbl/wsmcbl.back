using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.secretary;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("secretary")]
[Route("academy")]
[ApiController]
public class CreateEnrollmentActions(CreateEnrollmentController controller) : ActionsBase
{
    /// <summary>
    ///  Returns the list of active degrees
    /// </summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("degrees")]
    public async Task<IActionResult> getDegreeList()
    {
        var result = await controller.getDegreeList();
        return Ok(result.mapListToBasicDto());
    }

    /// <summary>
    ///  Returns the degree by id
    /// </summary>
    /// <response code="200">Return existing resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Degree not found.</response>
    [HttpGet]
    [Route("degrees/{degreeId}")]
    public async Task<IActionResult> getDegreeById([Required] string degreeId)
    {
        var result = await controller.getDegreeById(degreeId);
        var teacherList = await controller.getTeacherList();
        return Ok(result!.mapToDto(teacherList));
    }

    /// <summary>Create new enrollment.</summary>
    /// <param name="degreeId">
    /// The degreeId must be not empty.
    /// </param>
    /// <param name="quantity">
    /// The quantity value must be between 1 and 9.
    /// </param>
    /// <response code="201">If the resource is created.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource depends on another resource not found (degree) or quantity invalid.</response>
    [HttpPost]
    [Route("degrees/{degreeId}/enrollments")]
    public async Task<IActionResult> createEnrollment([FromQuery] string degreeId, [FromBody] int quantity)
    {
        var result = await controller.createEnrollments(degreeId, quantity);
        var teacherList = await controller.getTeacherList();
        return CreatedAtAction(nameof(getDegreeById), new { result.degreeId }, result.mapToDto(teacherList));
    }
}