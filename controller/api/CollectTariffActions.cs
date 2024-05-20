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
        StudentDtoTransformer service = new StudentDtoTransformer();
        var students = await controller.getStudentsList();
        return Ok(service.getStudentList(students));
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

        return Ok(new StudentDtoFull(student));
    }

    [HttpGet]
    [Route("tariffs")]
    public async Task<IActionResult> getTariffList()
    {
        return Ok(await controller.getTariffList());
    }

    [HttpPost]
    [Route("transactions")]
    public async Task saveTransaction([FromBody] TransactionDto transaction)
    {
        var service = new TransactionDtoTransformer();
        var element = service.getTransaction(transaction);
        await controller.saveTransaction(element);
    }
    
    [HttpGet]
    [Route("transactions/invoices/{studentId}")]
    public async Task<IActionResult> getLastTransaction(string studentId)
    {
        var lastTransaction = await controller.getLastTransactionByStudent(studentId);
        var student = await controller.getStudent(studentId);
        var cashier = await controller.getCashier(lastTransaction!.cashierId);
        
        var transactionOutput = new dto.output.TransactionDto(lastTransaction, student, cashier); 
        return Ok(transactionOutput);
    }
}