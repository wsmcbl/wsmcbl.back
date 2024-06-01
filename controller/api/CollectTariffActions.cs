using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.back.controller.business;
using wsmcbl.back.dto.input;
using wsmcbl.back.dto.output;
using wsmcbl.back.middleware.filter;

namespace wsmcbl.back.controller.api;

[Route("accounting")]
[ApiController]
public class CollectTariffActions(ICollectTariffController controller) : ControllerBase
{
    [HttpGet]
    [Route("students")]
    public async Task<IActionResult> getStudentList()
    {
        var students = await controller.getStudentsList();
        return Ok(students.mapToDto());
    } 
    
    [HttpGet]
    [Route("students/{studentId}")]
    public async Task<IActionResult> getStudentById([Required] string studentId)
    {
        var student = await controller.getStudent(studentId);
        
        return Ok(student!.mapToDto());
    }

    [HttpGet]
    [Route("tariffs")]
    public async Task<IActionResult> getTariffList()
    {
        return Ok(await controller.getTariffList());
    }

    
    /// <summary>
    /// Searches tariffs by student ID or state.
    /// </summary>
    /// <param name="q">The query string in the format "key:value". Supported keys are "student" and "state".
    /// Exist only state:overdue.</param>
    /// <returns>Returns the search results based on the provided query.</returns>
    /// <response code="200">Returns the search results.</response>
    /// <response code="400">If the query parameter is missing or not in the correct format.</response>
    [HttpGet]
    [Route("tariffs/search")]
    public async Task<IActionResult> getTariff([FromQuery] string q)
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

        var key = parts[0];
        var value = parts[1];

        return key.ToLower() switch
        {
            "student" => Ok(await controller.getTariffListByStudent(value)),
            "state" when value.Equals("overdue", StringComparison.CurrentCultureIgnoreCase)
                => Ok(await controller.getOverdueTariffList()),
            _ => BadRequest("Unknown search key.")
        };
    }
    
    [HttpPut]
    [Route("arrears/{tariffId:int}")]
    public async Task<IActionResult> applyArrears(int tariffId)
    {
        if (tariffId < 0)
        {
            return BadRequest("Invalid ID.");
        }

        await controller.applyArrears(tariffId);
        return Ok();
    }

    [HttpPost]
    [Route("transactions")]
    [ServiceFilter(typeof(ValidateModelFilter))]
    public async Task saveTransaction([FromBody] dto.input.TransactionDto transaction)
    {
        await controller.saveTransaction(transaction.toEntity());
    }
    
    [HttpGet]
    [Route("transactions/invoices/{studentId}")]
    public async Task<IActionResult> getInvoice([Required] string studentId)
    {
        var invoice = await controller.getLastTransactionByStudent(studentId);
        return Ok(invoice);
    }
}