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
    /// <response code="201">If the resource is created.</response>
    /// <response code="404">Resource depends on another resource not found (degree).</response>
    [HttpPost]
    [Route("degrees/enrollments")]
    public async Task<IActionResult> createEnrollment(EnrollmentToCreateDto dto)
    {
        var result = await controller.createEnrollments(dto.degreeId, dto.quantity);
        var teacherList = await controller.getTeacherList();
        return CreatedAtAction(nameof(getDegreeById), new { result.degreeId }, result.mapToDto(teacherList));
    }
}