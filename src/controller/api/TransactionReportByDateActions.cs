using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.accounting;
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
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("transactions/revenues")]
    public async Task<IActionResult> getReportByDate([FromQuery] int q)
    {
        if (q is < 1 or > 4)
        {
            throw new IncorrectDataBadRequestException("The query value must be 1,2,3 or 4.");
        }
        
        var response = new ReportByDateDto();

        response.setDateRage(await controller.getDateRange(q));
        response.setTransactionList(await controller.getTransactionList(q));
        response.userName = await controller.getUserName(getAuthenticatedUserId());
        
        var result = await controller.getSummary();
        response.setValidTransactionData(result[0]);
        response.setInvalidTransactionData(result[1]);
        
        return Ok(response);
    }
}