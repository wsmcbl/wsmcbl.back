using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.secretary;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin","secretary")]
[Route("secretary/enrollments")]
[ApiController]
public class EnrollStudentActions(EnrollStudentController controller) : ActionsBase
{
    /// <summary>Returns the list of students with registration solvency.</summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Registration tariff not found.</response>
    [HttpGet]
    [Route("students")]
    public async Task<IActionResult> getStudentsList()
    {
        var result = await controller.getStudentListWithSolvencyInRegistration();
        return Ok(result.mapToListBasicEnrollDto());
    }

    /// <summary>Returns the full student by id.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Student not found.</response>
    [HttpGet]
    [Route("students/{studentId}")]
    public async Task<IActionResult> getStudentById([Required] string studentId)
    {
        var result = await controller.getStudentById(studentId);
        var ids = await controller.getEnrollmentAndDiscountByStudentId(studentId);

        return Ok(result.mapToDto(ids));
    }

    /// <summary>Returns the list of degree with enrolments.</summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Schoolyear not found.</response>
    [HttpGet]
    [Route("students/{studentId}/degrees")]
    public async Task<IActionResult> getDegreeListByStudentId([Required] string studentId)
    {
        var result = await controller.getDegreeListByStudentId(studentId);
        return Ok(result.mapToListBasicDto());
    }

    /// <summary>Enroll student and update student information.</summary>
    /// <response code="200">Returns the modified resource.</response>
    /// <response code="400">Parameter is not valid.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource not found.</response>
    [HttpPut]
    [Route("")]
    public async Task<IActionResult> saveEnroll(EnrollStudentDto dto)
    {
        var result = await controller.saveEnroll(dto.getStudent(), dto.enrollmentId!, dto.isRepeating);
        
        await controller.updateStudentDiscount(dto.getStudentId(), dto.discountId);
        var ids = await controller.getEnrollmentAndDiscountByStudentId(dto.getStudentId());

        return Ok(result.mapToDto(ids));
    }

    /// <summary>Returns the enroll document of student.</summary>
    /// <response code="200">Return existing resources.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource depends on another resource not found.</response>
    [HttpGet]
    [Route("documents/{studentId}")]
    public async Task<IActionResult> getEnrollDocument([Required] string studentId)
    {
        var result = await controller.getEnrollDocument(studentId, getAuthenticatedUserId());
        return File(result, "application/pdf", $"{studentId}.enroll-sheet.pdf");
    }
}