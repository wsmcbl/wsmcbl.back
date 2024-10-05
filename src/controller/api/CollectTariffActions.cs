using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.dto.output;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.accounting;
using wsmcbl.src.exception;
using wsmcbl.src.middleware.filter;

namespace wsmcbl.src.controller.api;

[Route("accounting")]
[ApiController]
public class CollectTariffActions(ICollectTariffController controller) : ControllerBase
{
    /// <summary>
    ///  Returns the list of active students.
    /// </summary>
    /// <response code="200">Return existing resources (can be empty list).</response>
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
    /// <response code="404">Resource not found.</response>
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
        if (string.IsNullOrWhiteSpace(q))
        {
            throw new BadRequestException("Query parameter 'q' is required.");
        }

        var parts = q.Split(':');
        if (parts.Length != 2)
        {
            throw new BadRequestException("Query parameter 'q' is not in the correct format.");
        }

        var key = parts[0].ToLower();
        var value = parts[1].ToLower();

        return key switch
        {
            "student" => Ok(await controller.getTariffListByStudent(value)),
            "state" when value.Equals("overdue") => Ok(await controller.getOverdueTariffList()),
            _ => BadRequest("Unknown search key.")
        };
    }

    /// <summary>
    ///  Returns the list of tariff type.
    /// </summary>
    /// <response code="200">Return existing resources (can be empty list).</response>
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
    [ServiceFilter(typeof(ValidateModelFilterAttribute))]
    public async Task<IActionResult> saveTransaction([FromBody] TransactionDto dto)
    {
        var transactionId = await controller.saveTransaction(dto.toEntity(), dto.getDetailToApplyArrears());
        return CreatedAtAction(nameof(getInvoice), new { transactionId });
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
        var (transaction, student, cashier, generalBalance) = await controller.getFullTransaction(transactionId);
        var dto = transaction.mapToDto(student, cashier);
        dto.generalBalance = generalBalance;

        return Ok(dto);
    }
}