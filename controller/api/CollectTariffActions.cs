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

    [HttpGet]
    [Route("tariffs/search")]
    public async Task<IActionResult> getTariffByParameter([FromQuery] string q)
    { 
        if (string.IsNullOrEmpty(q))
        {
            return BadRequest("Query string 'q' is required.");
        }

        var queryParts = q.Split(':');
        if (queryParts.Length != 2)
        {
            return BadRequest("Query string 'q' is not in the correct format.");
        }

        var key = queryParts[0];
        var value = queryParts[1];
        
        return Ok(await controller.getTariffByStudent(value));
    }

    [HttpGet]
    [Route("arrears")]
    public async Task<IActionResult> getArrearsTariff([Required] [FromHeader] string schoolYear)
    {
        return Ok(await controller.getUnexpiredTariff(schoolYear));
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