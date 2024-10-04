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
    [HttpGet]
    [Route("students")]
    public async Task<IActionResult> getStudentList()
    {
        var students = await controller.getStudentsList();
        return Ok(students.mapListTo());
    } 
    
    [HttpGet]
    [Route("students/{studentId}")]
    public async Task<IActionResult> getStudentById([Required] string studentId)
    {
        try
        {
            var student = await controller.getStudent(studentId);
        
            return Ok(student.mapToDto());
        }
        catch
        {
            throw new EntityNotFoundException("Student", studentId);
        }
    }
    
    /// <param name="q">The query string in the format "key:value". Supported keys are "student:{id}" and "state:overdue".</param>
    /// <summary>Returns the search results based on the provided query.</summary>
    /// <response code="200">Returns the search results.</response>
    /// <response code="400">If the query parameter is missing or not in the correct format.</response>
    [HttpGet]
    [Route("tariffs/search")]
    public async Task<IActionResult> getTariffList([FromQuery] string q)
    { 
        if (string.IsNullOrWhiteSpace(q))
        {
            return BadRequest("Query parameter 'q' is required.");
        }

        var parts = q.Split(':');
        if (parts.Length != 2)
        {
            return BadRequest("Query parameter 'q' is not in the correct format.");
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
    
    [HttpGet]
    [Route("tariffs/types")]
    public async Task<ActionResult> getTariffTypeList()
    {
        var result = await controller.getTariffTypeList();

        return Ok(result);
    }

    /// <summary>Additional late fee applies.</summary>
    /// <response code=
    /// "200">Returns the search results.</response>
    /// <response code="400">If the query parameter is missing or not in the correct format.</response>
    /// <response code="461">If the resource is already update.</response>
    [HttpPut]
    [Route("arrears/{tariffId:int}")]
    public async Task<IActionResult> applyArrears(int tariffId)
    {
        if (tariffId < 0)
        {
            return BadRequest("Invalid ID.");   
        }

        try
        {
            await controller.applyArrears(tariffId);
            return Ok("Arrears applied");
        }
        catch(Exception e)
        {
            return BadRequest($"Server Error: {e.Message}");
        }
    }

    [HttpPost]
    [Route("transactions")]
    [ServiceFilter(typeof(ValidateModelFilterAttribute))]
    public async Task<IActionResult> saveTransaction([FromBody] TransactionDto dto)
    { 
        var transactionId = await controller.saveTransaction(dto.toEntity(), dto.getDetailToApplyArrears());
        return Ok(new { transactionId });
    }
    
    [HttpGet]
    [Route("transactions/invoices/{transactionId}")]
    public async Task<IActionResult> getInvoice([Required] string transactionId)
    {
        var (transaction, student, cashier, generalBalance) = await controller.getFullTransaction(transactionId);
        var dto = transaction.mapToDto(student, cashier);
        dto.generalBalance = generalBalance;

        return Ok(dto);
    }
}