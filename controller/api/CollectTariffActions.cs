using Microsoft.AspNetCore.Mvc;
using wsmcbl.back.controller.business;
using wsmcbl.back.dto.input;
using wsmcbl.back.dto.output;
using TransactionDto = wsmcbl.back.dto.input.TransactionDto;

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
    public async Task<IActionResult> getStudentById(string studentId)
    {
        var student = await controller.getStudent(studentId);
        if (student == null)
        {
            return NotFound();
        }

        return Ok(student.mapToDto());
    }

    [HttpGet]
    [Route("tariffs")]
    public async Task<IActionResult> getTariffList()
    {
        return Ok(await controller.getAllTariff());
    }

    [HttpGet]
    [Route("arrears")]
    public async Task<IActionResult> getArrearsTariff([FromHeader] string schoolYear)
    {
        return Ok(await controller.getUnexpiredTariff(schoolYear));
    }

    [HttpPut]
    [Route("arrears/{tariffId}")]
    public async Task applyArrears(int tariffId)
    {
        await controller.applyArrears(tariffId);
    }

    [HttpPost]
    [Route("transactions")]
    public async Task saveTransaction([FromBody] TransactionDto transaction)
    {
        await controller.saveTransaction(transaction.toEntity());
    }
    
    [HttpGet]
    [Route("transactions/invoices/{studentId}")]
    public async Task<IActionResult> getInvoice(string studentId)
    {
        var invoice = await controller.getLastTransactionByStudent(studentId);
        
        if (invoice is null)
            return BadRequest("This student no has transaction");
        
        return Ok(invoice);
    }
}