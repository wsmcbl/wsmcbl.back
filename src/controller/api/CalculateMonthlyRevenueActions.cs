using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.accounting;
using wsmcbl.src.exception;
using wsmcbl.src.middleware;
using wsmcbl.src.utilities;

namespace wsmcbl.src.controller.api;

[ApiController]
[Authorizer("report:read")]
[Route("accounting/revenue")]
public class CalculateMonthlyRevenueActions(CalculateMonthlyRevenueController controller) : ActionsBase 
{
    /// <summary>Returns total revenue received to date.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="400">If the date is not valid.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <param name="month">The month values must be "month-year" format, example "11-2024", "05-2025".</param>
    /// <remarks>A date before 2,000 is not accepted.</remarks>
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> getTotalReceived([Required] [FromQuery] string month)
    {
        var startDate = getStartDate(month);
        var result = await controller.getTotalReceived(startDate);
        return Ok(new RevenueByMonthDto(result, startDate));
    }
    
    /// <summary>Returns expected monthly revenue summary by month.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="400">If the date is not valid.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <param name="month">The month values must be "month-year" format, example "11-2024", "05-2025".</param>
    /// <remarks>A date before 2,000 is not accepted.</remarks>
    [HttpGet]
    [Route("expected-monthly")]
    public async Task<IActionResult> getExpectedMonthly([Required] [FromQuery] string month)
    {
        var result = await controller.getExpectedMonthly(getStartDate(month));
        return Ok(new ExpectedMonthlyDto(result));
    }
    
    /// <summary>Returns expected monthly received revenue summary for current month.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="400">If the date is not valid.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <param name="month">The month values must be "month-year" format, example "11-2024", "05-2025".</param>
    /// <remarks>A date before 2,000 is not accepted.</remarks>
    [HttpGet]
    [Route("expected-monthly/received")]
    public async Task<IActionResult> getExpectedMonthlyReceived([Required] [FromQuery] string month)
    {
        var result = await controller.getExpectedMonthly(getStartDate(month), true);
        return Ok(new ExpectedMonthlyReceivedDto(result));
    }

    private static DateTime getStartDate(string month)
    {
        var date = $"01-{month}";
        if (!TransactionReportByDateActions.hasDateFormat(date))
        {
            throw new IncorrectDataException("The date must be the correct format.");
        }

        return date.parseToDatetime();
    }
}