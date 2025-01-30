using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.accounting;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin", "cashier")]
[Route("accounting")]
[ApiController]
public class CollectTariffActions(CollectTariffController controller) : ActionsBase
{
    /// <summary>
    ///  Returns the list of active students.
    /// </summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    [HttpGet]
    [Route("students")]
    public async Task<IActionResult> getStudentList()
    {
        var students = await controller.getStudentsList();
        return Ok(students.mapListTo());
    }
    
    /// <summary>
    ///  Returns the student (active or not) by id.
    /// </summary>
    /// <response code="200">Return existing resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Student not found.</response>
    [HttpGet]
    [Route("students/{studentId}")]
    public async Task<IActionResult> getStudentById([Required] string studentId)
    {
        var student = await controller.getStudentById(studentId);
        return Ok(student.mapToDto());
    }

    /// <summary>
    /// Returns the search results based on the provided query.
    /// </summary>
    /// <param name="q">The query string in the format "key:value". Supported keys are "student:{id}" and "state:overdue".</param>
    /// <response code="200">Returns the search results.</response>
    /// <response code="400">Query parameter is missing or not in the correct format.</response>
    [HttpGet]
    [Route("tariffs/search")]
    public async Task<IActionResult> getTariffList([FromQuery] string q)
    {
        var parts = validateQueryValue(q);
        var key = parts[0].ToLower();
        var value = parts[1].ToLower();

        return key switch
        {
            "student" => Ok((await controller.getTariffListByStudent(value)).mapToListDto()),
            "state" when value.Equals("overdue") => Ok((await controller.getOverdueTariffList()).mapToListDto()),
            _ => BadRequest("Unknown search key.")
        };
    }

    /// <summary>
    ///  Returns the list of tariff type.
    /// </summary>
    /// <response code="200">Return existing resources (can be empty list).</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("tariffs/types")]
    public async Task<ActionResult> getTariffTypeList()
    {
        return Ok(await controller.getTariffTypeList());
    }

    /// <summary>Applies arrears to overdue tariff.</summary>
    /// <response code="200">Returns the modified resource.</response>
    /// <response code="400">Parameter is not valid.</response>
    /// <response code="404">Resource not found.</response>
    [HttpPut]
    [Route("arrears/{tariffId:int}")]
    public async Task<IActionResult> applyArrears(int tariffId)
    {
        return Ok(await controller.applyArrears(tariffId));
    }

    /// <summary>Create new transaction resource.</summary>
    /// <response code="201">If the new resource is created.</response>
    /// <response code="404">Resource depends on another resource not found.</response>
    [HttpPost]
    [Route("transactions")]
    public async Task<IActionResult> saveTransaction([FromBody] TransactionDto dto)
    {
        var transaction = await controller.saveTransaction(dto.toEntity(), dto.getDetailToApplyArrears());
        return CreatedAtAction(nameof(controller.saveTransaction), transaction.mapToDto());
    }

    
    /// <summary>
    ///  Returns the invoice document of transactionId.
    /// </summary>
    /// <response code="200">Return existing resources.</response>
    /// <response code="404">Resource depends on another resource not found.</response>
    [HttpGet]
    [Route("documents/invoices/{transactionId}")]
    public async Task<IActionResult> getInvoice([Required] string transactionId)
    {
        var result = await controller.getInvoiceDocument(transactionId);
        return File(result, "application/pdf", $"{transactionId}.invoice.pdf");
    }
}