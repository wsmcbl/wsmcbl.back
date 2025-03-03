using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[Route("config/backups")]
[ApiController]
public class CreateBackupActions(CreateBackupsController controller) : ActionsBase
{
    /// <summary>Returns current backup document.</summary>
    /// <response code="200">Return existing resources.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource depends on another resource not found.</response>
    [HttpGet]
    [Route("current")]
    [ResourceAuthorizer("admin")]
    public IActionResult getBackupDocument()
    {
        var result = controller.getCurrentBackupDocument();
        return File(result.data, "application/pdf", result.name);
    }
}