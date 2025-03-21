using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.accounting;
using wsmcbl.src.middleware;
using wsmcbl.src.model;

namespace wsmcbl.src.controller.api;

[Route("accounting")]
[ApiController]
public class CollectTariffActions(CollectTariffController controller) : ActionsBase
{
    /// <summary>Returns paged list of active students.</summary>
    /// <remarks>Values for sortBy: studentId, fullName, isActive, tutor, schoolyear and enrollment.</remarks>
    /// <response code="200">Returns a paged list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("students")]
    [ResourceAuthorizer("student:read")]
    public async Task<IActionResult> getStudentList([FromQuery] PagedRequest request)
    {
        request.checkSortByValue(["studentId", "fullName", "isActive", "tutor", "schoolyear", "enrollment"]);
        
        var result = await controller.getStudentList(request);
        
        var pagedResult = new PagedResult<BasicStudentDto>(result.data.mapToList());
        pagedResult.setup(result);
        
        return Ok(pagedResult);
    }
    
    /// <summary>Returns the student (active or not) by id.</summary>
    /// <response code="200">Return existing resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Student not found.</response>
    [HttpGet]
    [Route("students/{studentId}")]
    [ResourceAuthorizer("student:read")]
    public async Task<IActionResult> getStudentById([Required] string studentId)
    {
        var student = await controller.getStudentById(studentId);
        return Ok(student.mapToDto());
    }

    /// <summary>Returns the tariff list by student id.</summary>
    /// <response code="200">Returns the search results.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Student not found.</response>
    [HttpGet]
    [Route("students/{studentId}/tariffs")]
    [ResourceAuthorizer("student:read")]
    public async Task<IActionResult> getTariffListByStudentId([Required] string studentId)
    {
        var result = await controller.getTariffListByStudent(studentId);
        return Ok(result.mapToListDto());
    }

    /// <summary>Create new transaction resource.</summary>
    /// <response code="201">If the new resource is created.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource depends on another resource not found.</response>
    [HttpPost]
    [Route("transactions")]
    [ResourceAuthorizer("transaction:create")]
    public async Task<IActionResult> saveTransaction([FromBody] TransactionDto dto)
    {
        var transaction = await controller.saveTransaction(dto.toEntity(), dto.getDetailToApplyArrears());
        return CreatedAtAction(nameof(controller.saveTransaction), transaction.mapToDto());
    }
}