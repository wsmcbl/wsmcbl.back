using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.management;
using wsmcbl.src.exception;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[Route("management/partials")]
[ApiController]
public class EnablePartialGradeRecordingActions(EnablePartialGradeRecordingController controller) : ActionsBase
{
    /// <summary>Get partial list by current schoolyear.</summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("")]
    [ResourceAuthorizer("partial:read")]
    public async Task<IActionResult> getPartialList()
    {
        var result = await controller.getPartialList();
        return Ok(result.mapListToDto());
    }

    /// <summary>Enable or disable partial grade recording by id.</summary>
    /// <param name="partialId">Partial id, the partial must be active.</param>>
    /// <param name="enable">A boolean value, if false the other parameter is not needed.</param>>
    /// <param name="deadline">A Datetime value, it must be a datetime greater than the current one.</param>>
    /// <response code="200">Returns the modified resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpPut]
    [Route("{partialId:int}")]
    [ResourceAuthorizer("partial:update")]
    public async Task<IActionResult> enablePartialGradeRecording([Required] int partialId,
        [Required] [FromQuery] bool enable, [FromQuery] string? deadline)
    {
        if (!enable)
        {
            await controller.disableGradeRecording(partialId);
            return Ok();
        }
        
        if (string.IsNullOrWhiteSpace(deadline))
        {
            throw new BadRequestException("The deadline must be provided.");
        }

        if (!DateTime.TryParse(deadline, out var date))
        {
            throw new BadRequestException("The deadline is not valid.");
        }
        
        await controller.enableGradeRecording(partialId, date);
        return Ok();
    }
}