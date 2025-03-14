using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.secretary;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[Route("secretary/schoolyears")]
[ApiController]
public class CreateSchoolyearActions(CreateSchoolyearController controller) : ControllerBase
{
    /// <summary>Returns the list of schoolyear.</summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("")]
    [ResourceAuthorizer("schoolyear:read")]
    public async Task<IActionResult> getSchoolyearList()
    {
        var list = await controller.getSchoolyearList();
        return Ok(list.mapListToDto());
    }
    
    /// <summary>Returns the list of schoolyear.</summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("{schoolyearId}")]
    [ResourceAuthorizer("schoolyear:read")]
    public async Task<IActionResult> getSchoolyearById([Required] string schoolyearId)
    {
        var result = await controller.getSchoolyearById(schoolyearId);
        return Ok(result.mapToDto());
    }
    
    /// <summary>Create new schoolyear.</summary>
    /// <response code="201">If the resource is created.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource depends on another resource not found (degree).</response>
    [HttpPost]
    [Route("")]
    [ResourceAuthorizer("schoolyear:create")]
    public async Task<IActionResult> createSchoolyear(SchoolyearToCreateDto dto)
    {
        await controller.createSchoolyear();
        await controller.createPartialList(dto.getPartialList());
        await controller.createSubjectList();
        await controller.createTariffList(dto.getTariffList());
        await controller.createExchangeRate();

        var result = controller.getSchoolyearCreated();
        return CreatedAtAction(null, result.mapToDto());
    }
    
    /// <summary>Create new tariff catalog.</summary>
    /// <response code="201">If the resource is created.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource depends on another resource not found (degree).</response>
    [HttpPost]
    [Route("tariffs")]
    [ResourceAuthorizer("schoolyear:read")]
    public async Task<IActionResult> createTariff(TariffDataDto dto)
    {
        var result = await controller.createTariff(dto.toEntity());
        return CreatedAtAction(null, result);
    }
}