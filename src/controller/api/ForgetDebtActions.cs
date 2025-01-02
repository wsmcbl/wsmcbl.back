using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin", "cashier")]
[Route("accounting")]
[ApiController]
public class ForgetDebtActions(ForgetDebtController controller) : ActionsBase
{
    /// <summary>
    ///  Update forgive a debt.
    /// </summary>
    /// <response code="200">Returns the edited resource.</response>
    /// <response code="404">If resource not exist(student or tariff).</response>
    /// <response code="409">If the debt is already paid.</response>
    [HttpPut]
    [Route("debts")]
    public async Task<IActionResult> forgiveADebt([FromQuery] string studentId, [FromQuery] int tariffId)
    {
        return Ok(await controller.forgiveADebt(studentId, tariffId));
    }
}