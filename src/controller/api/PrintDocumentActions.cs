using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin", "secretary")]
[Route("secretary")]
[ApiController]
public class PrintDocumentActions(PrintDocumentController controller) : ActionsBase
{
    /// <summary>
    ///  Returns the assistance document of all degrees.
    /// </summary>
    /// <response code="200">Return existing resources.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource depends on another resource not found.</response>
    [HttpGet]
    [Route("degrees/documents")]
    public async Task<IActionResult> getAssistanceDocument()
    {
        var result = await controller.getAssistanceListDocument();
        return File(result, "application/pdf", "assistance-list.pdf");
    }    
}