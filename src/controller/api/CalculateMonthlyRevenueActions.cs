using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ApiController]
[Authorizer("report:read")]
[Route("accounting/revenue")]
public class CalculateMonthlyRevenueActions(CalculateMonthlyRevenueController controller) : ActionsBase 
{
    /// <summary>Returns expected monthly revenue summary by month.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <param name="month">The month values must be "month-year" format, example "11-2024", "05-2025".</param>
    [HttpGet]
    [Route("expected-monthly")]
    public async Task<IActionResult> getExpectedMonthly([Required] [FromQuery] string month)
    {
        var result = await controller.getExpectedMonthly(getStartDate(month));
        return Ok(result);
    }
    
    /// <summary>Returns expected monthly received revenue summary for current month.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <param name="month">The month values must be "month-year" format, example "11-2024", "05-2025".</param>
    [HttpGet]
    [Route("expected-monthly/received")]
    public async Task<IActionResult> getExpectedMonthlyReceived([Required] [FromQuery] string month)
    {
        var result = await controller.getExpectedMonthlyReceived(getStartDate(month));
        return Ok(result);
    }
    
    /// <summary>Returns total revenue received to date.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <param name="month">The month values must be "month-year" format, example "11-2024", "05-2025".</param>
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> getTotalReceived([Required] [FromQuery] string month)
    {
        var result = await controller.getTotalReceived(getStartDate(month));
        return Ok(result);
    }

    private DateTime getStartDate(string month)
    {
        return DateTime.Parse($"01-{month}");
    }
}