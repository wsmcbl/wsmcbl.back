using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ApiController]
[Authorizer("report:read")]
[Route("accounting/revenue")]
public class CalculateMonthlyRevenueActions(CalculateMonthlyRevenueController controller) : ActionsBase 
{
    /// <summary>Returns expected monthly revenue summary for current month.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("expected-monthly")]
    public async Task<IActionResult> getExpectedMonthly()
    {
        var result = await controller.getExpectedMonthly();
        return Ok(result);
    }
    
    /// <summary>Returns expected monthly received revenue summary for current month.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("expected-monthly/received")]
    public async Task<IActionResult> getExpectedMonthlyReceived()
    {
        var result = await controller.getExpectedMonthlyReceived();
        return Ok(result);
    }
    
    /// <summary>Returns total revenue received to date.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> getTotalReceived()
    {
        var result = await controller.getTotalReceived();
        return Ok(result);
    }
}