using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.exception;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin", "secretary", "cashier")]
[Route("accounting")]
[ApiController]
public class TransactionReportByDateActions(ITransactionReportByDateController controller) : ActionsBase
{
    /// <summary>
    /// Returns summary list of transactions and revenues by date.
    /// </summary>
    /// <param name="q">The value must be 1, 2, 3 or 4. Value corresponding to today, yesterday, monthly and yearly.</param>
    /// <response code="200">Returns a list, the list can be empty.</response>
    [HttpGet]
    [Route("transactions/revenues")]
    public async Task<IActionResult> getReportByDate([FromQuery] int q)
    {
        if (q is < 1 or > 4)
        {
            throw new IncorrectDataBadRequestException("The query value must be 1,2,3 or 4.");
        }
        
        var list = await controller.getReportByDate(q);
        return Ok(list);
    }
}