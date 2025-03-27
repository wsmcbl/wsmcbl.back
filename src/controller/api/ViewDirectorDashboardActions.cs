using Microsoft.AspNetCore.Mvc;

namespace wsmcbl.src.controller.api;

[Route("management")]
[ApiController]
public class ViewDirectorDashboardActions(ViewDirectorDashboardController controller) : ActionsBase
{
    /// <summary>Get enrolled students.</summary>
    /// <response code="200">Return the value</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("permissions")]
    public async Task<IActionResult> getPermissionList()
    {
        return Ok(await controller.getEnrolledStudent());
    }
}