using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.accounting;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[Route("accounting")]
[ApiController]
public class CollectTariffActions(CollectTariffController controller) : ActionsBase
{
    /// <summary>Returns the list of active students.</summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("students")]
    [ResourceAuthorizer("student:read")]
    public async Task<IActionResult> getStudentList()
    {
        var students = await controller.getStudentsList();
        return Ok(students.mapListTo());
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

    
    /// <summary>Returns the invoice document by transactionId.</summary>
    /// <response code="200">Return existing resources.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource depends on another resource not found.</response>
    [HttpGet]
    [Route("documents/invoices/{transactionId}")]
    [ResourceAuthorizer("transaction:read")]
    public async Task<IActionResult> getInvoice([Required] string transactionId)
    {
        var result = await controller.getInvoiceDocument(transactionId);
        return File(result, "application/pdf", $"{transactionId}.invoice.pdf");
    }
    
    /// <summary>Applies arrears to overdue tariff.</summary>
    /// <response code="200">Returns the modified resource.</response>
    /// <response code="400">Parameter is not valid.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource not found.</response>
    [HttpPut]
    [Route("tariffs/{tariffId:int}")]
    [ResourceAuthorizer("tariff:update")]
    public async Task<IActionResult> applyArrears(int tariffId)
    {
        return Ok(await controller.applyArrears(tariffId));
    }
    
    /// <summary>Returns the list of tariff type.</summary>
    /// <response code="200">Return existing resources (can be empty list).</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("tariffs/types")]
    [ResourceAuthorizer("tariff:read")]
    public async Task<ActionResult> getTariffTypeList()
    {
        return Ok(await controller.getTariffTypeList());
    }
    
    /// <summary>Returns overdue tariff list.</summary>
    /// <response code="200">Returns the search results.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("tariffs/overdues")]
    [ResourceAuthorizer("tariff:read")]
    public async Task<IActionResult> getOverdueTariffList()
    {
        var result = await controller.getOverdueTariffList();
        return Ok(result.mapToListDto());
    }
}