using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.secretary;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin","secretary", "cashier","teacher", "principal")] // Temporal
[Route("secretary/configurations/schoolyears")]
[ApiController]
public class CreateSchoolyearActions(UpdateOfficialEnrollmentController controller) : ControllerBase
{
    /// <summary>Returns the search results based on the provided query.</summary>
    /// <param name="q">
    /// The query string in the format "value".
    /// Supported values are "all" and "new".
    /// </param>
    /// <response code="200">Returns the search results.</response>
    /// <response code="400">If the query parameter is missing or not in the correct format.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> getSchoolYears([Required] [FromQuery] string q)
    {
        switch (q.ToLower())
        {
            case "all":
            {
                var list = await controller.getSchoolYearList();
                return Ok(list.mapListToDto());
            }
            case "new":
            {
                var schoolyearBaseInformation = await controller.getNewSchoolYearInformation();
                return Ok(schoolyearBaseInformation.mapToDto());
            }
            default:
                return NotFound("Unknown type value.");
        }
    }


    /// <summary>Create new schoolyear.</summary>
    /// <remarks>
    /// Property semester in partialList can be 1 or 2.
    /// Property exchangeRate can be double.
    /// </remarks>
    /// <response code="201">If the resource is created.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource depends on another resource not found (degree).</response>
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> createSchoolYear(SchoolYearToCreateDto dto)
    {
        var result = await controller.createSchoolYear(dto.getGradeList(), dto.getTariffList());
        await controller.createSemester(result, dto.getPartialList());
        await controller.createExchangeRate(result, dto.exchangeRate);

        return CreatedAtAction(null, result);
    }

    /// <summary>Create new subject catalog.</summary>
    /// <response code="201">If the resource is created.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource depends on another resource not found (degree).</response>
    [HttpPost]
    [Route("subjects")]
    public async Task<IActionResult> createSubject(SubjectDataDto dto)
    {
        var result = await controller.createSubject(dto.toEntity());
        return CreatedAtAction(null, result);
    }

    /// <summary>Create new tariff catalog.</summary>
    /// <response code="201">If the resource is created.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource depends on another resource not found (degree).</response>
    [HttpPost]
    [Route("tariffs")]
    public async Task<IActionResult> createTariff(TariffDataDto dto)
    {
        var result = await controller.createTariff(dto.toEntity());
        return CreatedAtAction(null, result);
    }    
}