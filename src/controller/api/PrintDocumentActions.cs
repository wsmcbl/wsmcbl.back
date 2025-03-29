using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[Route("secretary")]
[ApiController]
public class PrintDocumentActions(PrintDocumentController controller) : ActionsBase
{
    /// <summary>Returns the official enrollment list document of all degrees.</summary>
    /// <response code="200">Return existing resources.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource depends on another resource not found.</response>
    [HttpGet]
    [Route("degrees/documents")]
    [Authorizer("report:read")]
    public async Task<IActionResult> getOfficialEnrollmentListDocument()
    {
        var result = await controller.getOfficialEnrollmentListDocument(getAuthenticatedUserId());
        return File(result, "application/pdf", "official-enrollment-list.pdf");
    }    
}