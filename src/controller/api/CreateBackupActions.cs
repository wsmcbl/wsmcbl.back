using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[Route("config/backups")]
[ApiController]
public class CreateBackupActions : ActionsBase
{
    /// <summary>Returns current backup in SQL format.</summary>
    /// <response code="200">Return existing resources.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource depends on another resource not found.</response>
    [HttpGet]
    [Route("current/export")]
    [Authorizer("admin")]
    public async Task<IActionResult> getBackupDocument()
    {
        var result = await CreateBackupsController.getCurrentBackupDocument();
        return File(result.data, "application/pdf", result.name);
    }
}