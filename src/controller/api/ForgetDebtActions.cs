using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.accounting;
using wsmcbl.src.exception;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin", "cashier")]
[Route("accounting")]
[ApiController]
public class ForgetDebtActions(ForgetDebtController controller) : ActionsBase
{
    /// <summary>
    ///  Returns the list of debts by student id
    /// </summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("debts")]
    public async Task<IActionResult> getDebtListByStudent([FromQuery] string studentId)
    {
        var result = await controller.getDebtListByStudent(studentId);
        return Ok(result.mapToListDto());
    }
    
    /// <summary>
    ///  Update forgive a debt.
    /// </summary>
    /// <response code="200">Returns the edited resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">If resource not exist(student or tariff).</response>
    /// <response code="409">If the debt is already paid.</response>
    [HttpPut]
    [Route("debts")]
    public async Task<IActionResult> forgiveADebt(ForgetDebtDto dto)
    {
        if (!dto.authorizationToken.Equals("36987"))
        {
            throw new ForbiddenException("Incorrect authorization code.");
        }
        
        return Ok(await controller.forgiveADebt(dto.studentId, dto.tariffId));
    }
}