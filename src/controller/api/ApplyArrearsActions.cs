using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.accounting;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[Route("accounting/tariffs")]
[ApiController]
public class ApplyArrearsActions(ApplyArrearsController controller) : ActionsBase
{
    /// <summary>Returns overdue tariff list.</summary>
    /// <response code="200">Returns the search results.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("overdues")]
    [ResourceAuthorizer("tariff:read")]
    public async Task<IActionResult> getOverdueTariffList()
    {
        var result = await controller.getOverdueTariffList();
        return Ok(result.mapToListDto());
    }
    
    /// <summary>Applies arrears to overdue tariff.</summary>
    /// <response code="200">Returns the modified resource.</response>
    /// <response code="400">Parameter is not valid.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource not found.</response>
    [HttpPut]
    [Route("{tariffId:int}")]
    [ResourceAuthorizer("tariff:update")]
    public async Task<IActionResult> applyArrears(int tariffId)
    {
        return Ok(await controller.applyArrears(tariffId));
    }
    
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