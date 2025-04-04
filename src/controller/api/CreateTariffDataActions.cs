using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.dto.secretary;
using wsmcbl.src.controller.business;
using wsmcbl.src.middleware;

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
    [Authorizer("catalog:read")]
    public async Task<IActionResult> getTariffDataList()
    {
        var result = await controller.getTariffDataList();
        return Ok(result.mapListToDto());
    }

    /// <summary>Update tariff catalog.</summary>
    /// <remarks>The tariffDataId is not necessary.</remarks>
    /// <response code="200">If the resource is updated.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource not found.</response>
    [HttpPut]
    [Route("{tariffId:int}")]
    [Authorizer("catalog:update")]
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
    [Authorizer("catalog:create")]
    public async Task<IActionResult> createTariffData(TariffDataDto dto)
    {
        var result = await controller.createTariffData(dto.toEntity());
        return CreatedAtAction(null, new TariffDataDto(result));
    }
}