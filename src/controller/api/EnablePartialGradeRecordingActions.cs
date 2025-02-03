using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[Route("management")]
[ApiController]
public class EnablePartialGradeRecordingActions(EnablePartialGradeRecordingController controller) : ActionsBase
{
    /// <summary>Get partial list by current schoolyear.</summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("partial/list")]
    [ResourceAuthorizer("partial:read")]
    public async Task<IActionResult> getUser([Required] string userId)
    {
        return Ok(await controller.getPartialList());
    }
}