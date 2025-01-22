using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.accounting;
using wsmcbl.src.exception;
using wsmcbl.src.middleware;
using wsmcbl.src.utilities;

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
            throw new IncorrectDataBadRequestException("Some of the dates are not in the correct format.");
        }

        var dates = parseToDateTime(start, end);
        var transactionList = await controller.getTransactionList(dates.start, dates.end);
        
        var response = new ReportByDateDto();
        response.setDateRange(dates.start, dates.end);
        response.setTransactionList(transactionList);
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

    private static (DateTime start, DateTime end) parseToDateTime(string start, string end)
    {
        var startDate = DateTime.ParseExact(start, "dd-MM-yyyy", CultureInfo.InvariantCulture);
        var endDate = DateTime.ParseExact(end, "dd-MM-yyyy", CultureInfo.InvariantCulture);

        if (startDate.Date < endDate.Date)
        {
            throw new BadRequestException("The date range is not valid.");
        }

        startDate = startDate.setHours(6);
        
        endDate = endDate.setHours(6);
        endDate = endDate.Date.AddDays(-1).AddSeconds(-1);
        
        return (startDate, endDate);
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