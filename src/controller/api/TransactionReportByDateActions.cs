using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.accounting;
using wsmcbl.src.exception;
using wsmcbl.src.middleware;
using wsmcbl.src.model;
using wsmcbl.src.utilities;

namespace wsmcbl.src.controller.api;

[Route("accounting/transactions")]
[ApiController]
public class TransactionReportByDateActions(TransactionReportByDateController controller) : ActionsBase
{
    /// <summary>Returns summary list of transactions and revenues by date.</summary>
    /// <remarks> The date values must be "day-month-year" format, example "25-01-2025".</remarks>
    /// <remarks> A date before 2,000 is not accepted.</remarks>
    /// <param name="request">Paged request</param>
    /// <param name="request.from">The default time is set to 00:00 hours.</param>
    /// <param name="request.to">
    /// The default time is set to 23:59.
    /// If the date entered corresponds to the current date,
    /// the time will be adjusted to the time at which the query is made.
    /// </param>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="400">If any of the dates are not valid.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("revenues")]
    [ResourceAuthorizer("report:read")]
    public async Task<IActionResult> getReportByDate([FromQuery] TransactionReportViewPagedRequest request)
    {
        if (!hasDateFormat(request.from) || !hasDateFormat(request.to))
        {
            throw new IncorrectDataBadRequestException("Some of the dates are not in the correct format.");
        }
        
        var result = await controller.getTransactionList(request);
        
        var pagedResult = new PagedReportByDateDto(result.data.mapToListDto());
        pagedResult.setup(result);
        
        var dates = parseToDateTime(request.from!, request.to!);
        var summaryReport = await controller.getSummary(dates.from, dates.to);
        
        pagedResult.setValidTransactionData(summaryReport[0]);
        pagedResult.setInvalidTransactionData(summaryReport[1]);
        
        pagedResult.setDateRange(dates.from, dates.to);
        pagedResult.setUserName(await controller.getUserName(getAuthenticatedUserId()));
        
        return Ok(pagedResult);
    }

    public static bool hasDateFormat(string? value)
    {
        if (value == null)
        {
            return false;
        }
        
        const int minYear = 2000;
        const int maxYear = 2100;

        try
        {
            var date = DateTime.ParseExact(value, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            return date.Year is >= minYear and <= maxYear;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    public static (DateTime from, DateTime to) parseToDateTime(string from, string to)
    {
        var startDate = DateTime.ParseExact(from, "dd-MM-yyyy", CultureInfo.InvariantCulture);
        var endDate = DateTime.ParseExact(to, "dd-MM-yyyy", CultureInfo.InvariantCulture);

        if (startDate.Date > endDate.Date)
        {
            throw new BadRequestException("The date range is not valid.");
        }

        startDate = startDate.setHours(6);
        
        endDate = isToday(endDate) ? DateTime.UtcNow :
            endDate.setHours(0).Date.AddDays(1).AddHours(6).AddSeconds(-1);
        
        return (startDate, endDate);
    }

    private static bool isToday(DateTime date) => date.Date == DateTime.Today.Date;
    

    /// <summary>Returns the list of tariff type.</summary>
    /// <response code="200">Return existing resources (can be empty list).</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("types")]
    [ResourceAuthorizer("tariff:read")]
    public async Task<ActionResult> getTariffTypeList()
    {
        return Ok(await controller.getTariffTypeList());
    }
}