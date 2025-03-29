using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[Authorizer("report:read")]
[Route("accounting")]
[ApiController]
public class GenerateDebtorReportActions(GenerateDebtorReportController controller) : ActionsBase
{
    /// <summary>Returns the invoice document by transactionId.</summary>
    /// <response code="200">Return existing resources.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource depends on another resource not found.</response>
    [HttpGet]
    [Route("documents/debtor")]
    public async Task<IActionResult> getDebtorReport()
    {
        var result = await controller.getDebtorReport(getAuthenticatedUserId());
        return File(result, "application/pdf", "debtor.report.pdf");
    }
}