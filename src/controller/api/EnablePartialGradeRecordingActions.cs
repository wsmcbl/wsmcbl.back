using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.management;
using wsmcbl.src.exception;
using wsmcbl.src.middleware;
using wsmcbl.src.utilities;

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
    [Authorizer("partial:read")]
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
    [Authorizer("partial:update")]
    public async Task<IActionResult> enablePartialGradeRecording([Required] int partialId,
        [Required] [FromQuery] bool enable, [FromQuery] DateTime? deadline)
    {
        if (!enable)
        {
            await controller.disableGradeRecording(partialId);
            return Ok();
        }
        
        if (deadline == null)
        {
            throw new IncorrectDataException("deadline", "Check that the value is not null.");
        }

        await controller.checkForPartialEnabledOrFail();
        await controller.enableGradeRecording(partialId, ((DateTime)deadline).ToUniversalTime());
        return Ok();
    }

    /// <summary>Activate or deactivates partial by id.</summary>
    /// <param name="partialId">Partial id, the partial must be active.</param>>
    /// <param name="isActive">A boolean value.</param>>
    /// <response code="200">Returns the modified resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpPut]
    [Route("{partialId:int}/activate")]
    [Authorizer("partial:update")]
    public async Task<IActionResult> activatePartial([Required] int partialId, [Required] [FromQuery] bool isActive)
    {
        await controller.activatePartial(partialId, isActive);
        return Ok();
    }
    
    /// <summary>Get a partial with grade recording enabled.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">If the partial not found.</response>
    [HttpGet]
    [Route("enables")]
    [Authorizer("partial:read")]
    public async Task<IActionResult> getPartialEnabled()
    {
        var result = await controller.getPartialEnabled();
        return Ok(new {result.partialId, result.label, semester = result.getSemesterLabel(), result.gradeRecordDeadline});
    }
}
