using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.accounting;
using wsmcbl.src.exception;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin", "secretary", "cashier")]
[Route("accounting")]
[ApiController]
public class TransactionReportByDateActions(TransactionReportByDateController controller) : ActionsBase
{
    /// <summary>
    /// Returns summary list of transactions and revenues by date.
    /// </summary>
    /// <param name="start">The value must be "day-month-year".</param>
    /// <param name="end">The value must be "day-month-year".</param>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("transactions/revenues")]
    public async Task<IActionResult> getReportByDate([FromQuery] [Required] string start, [FromQuery] string end)
    {
        if (!hasDateFormat(start) || !hasDateFormat(end))
        {
            throw new BadRequestException("The dates has not valid format.");
        }
        
        var response = new ReportByDateDto();
        response.setDateRage(controller.getDateRange(1));
        response.setTransactionList(await controller.getTransactionList(new DateTime(), new DateTime()));
        response.userName = await controller.getUserName(getAuthenticatedUserId());
        
        var result = controller.getSummary();
        response.setValidTransactionData(result[0]);
        response.setInvalidTransactionData(result[1]);
        
        return Ok(response);
    }

    private bool hasDateFormat(string start)
    {
        return true;
    }

    /// <summary>
    ///  Returns the list of tariff type.
    /// </summary>
    /// <response code="200">Return existing resources (can be empty list).</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("transactions/types")]
    public async Task<ActionResult> getTariffTypeList()
    {
        return Ok(await controller.getTariffTypeList());
    }
}