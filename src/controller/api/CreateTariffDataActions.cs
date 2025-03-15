using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.dto.secretary;
using wsmcbl.src.controller.business;
using wsmcbl.src.middleware;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.api;

[Route("secretary/catalogs/tariffs")]
[ApiController]
public class CreateTariffDataActions(CreateTariffDataController controller) : ControllerBase
{
    /// <summary>Returns tariff catalog list.</summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("")]
    [ResourceAuthorizer("catalog:read")]
    public async Task<IActionResult> getTariffDataList()
    {
        return Ok(await controller.getTariffDataList());
    }

    /// <summary>Update tariff data.</summary>
    /// <remarks>The tariffDataId is not necessary.</remarks>
    /// <response code="200">If the resource is updated.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource not found.</response>
    [HttpPut]
    [Route("{tariffId:int}")]
    [ResourceAuthorizer("catalog:update")]
    public async Task<IActionResult> updateTariffData(int tariffId, TariffDataDto dto)
    {
        await controller.updateTariffData(dto.toEntity(tariffId));
        return Ok();
    }

    /// <summary>Create new tariff catalog.</summary>
    /// <response code="201">If the resource is created.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource depends on another resource not found (degree).</response>
    [HttpPost]
    [Route("")]
    [ResourceAuthorizer("catalog:create")]
    public async Task<IActionResult> createTariffData(TariffDataDto dto)
    {
        var result = await controller.createTariffData(dto.toEntity());
        return CreatedAtAction(null, result);
    }
    
    /// <summary>Returns tariff type list.</summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("types")]
    [ResourceAuthorizer("catalog:read")]
    public async Task<IActionResult> getTariffTypeList()
    {
        return Ok(await controller.getTariffTypeList());
    }
}