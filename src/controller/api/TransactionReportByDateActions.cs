using System.ComponentModel.DataAnnotations;
using System.Globalization;
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
            throw new IncorrectDataBadRequestException("The dates has not valid format.");
        }

        var response = new ReportByDateDto();
        response.setDateRage(controller.getDateRange(start, end));
        response.setTransactionList(await controller.getTransactionList(start, end));
        response.userName = await controller.getUserName(getAuthenticatedUserId());

        var result = controller.getSummary();
        response.setValidTransactionData(result[0]);
        response.setInvalidTransactionData(result[1]);

        return Ok(response);
    }

    private bool hasDateFormat(string value)
    {
        var minYear = 2000;
        var maxYear = 2100;

        try
        {
            DateTime date = DateTime.ParseExact(value, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            var year = date.Year;
            return year >= minYear && year <= maxYear;
        }
        catch (FormatException)
        {
            return false;
        }
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