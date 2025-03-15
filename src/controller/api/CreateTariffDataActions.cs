using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.dto.secretary;
using wsmcbl.src.controller.business;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[Route("secretary/catalogs")]
[ApiController]
public class CreateTariffDataActions(CreateTariffDataController controller) : ControllerBase
{
    /// <summary>Create new tariff catalog.</summary>
    /// <response code="201">If the resource is created.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource depends on another resource not found (degree).</response>
    [HttpPost]
    [Route("tariffs")]
    [ResourceAuthorizer("catalog:read")]
    public async Task<IActionResult> createTariff(TariffDataDto dto)
    {
        var result = await controller.createTariff(dto.toEntity());
        return CreatedAtAction(null, result);
    }
}