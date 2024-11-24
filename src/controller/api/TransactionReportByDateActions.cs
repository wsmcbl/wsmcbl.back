using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin", "secretary", "cashier")]
[Route("accounting")]
[ApiController]
public class TransactionReportByDateActions(ITransactionReportByDateController controller) : ActionsBase
{
    /// <summary>
    /// Return the list of 
    /// </summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    [HttpGet]
    [Route("transactions/report")]
    public async Task<IActionResult> getReportByDate()
    {
        var list = await controller.getReportByDate();
        return Ok(list);
    }
}