using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.accounting;
using wsmcbl.src.exception;
using wsmcbl.src.middleware;
using wsmcbl.src.model;

namespace wsmcbl.src.controller.api;

[Route("accounting/students/{studentId}/debts")]
[ApiController]
public class ForgetDebtActions(ForgetDebtController controller) : ActionsBase
{
    /// <summary>Returns the list of debts by student id.</summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("")]
    [ResourceAuthorizer("debt:read")]
    public async Task<IActionResult> getPaginatedDebtByStudentId([Required] string studentId, [FromQuery] PagedRequest request)
    {
        var result = await controller.getPaginatedDebtByStudentId(studentId, request);
        
        var pagedResult = new PagedResult<DebtDto>(result.data.mapToListDto());
        pagedResult.setup(result);
        
        return Ok(pagedResult);
    }
    
    /// <summary>Update forgive a debt.</summary>
    /// <response code="200">Returns the edited resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">If resource not exist (student or tariff).</response>
    /// <response code="409">If the debt is already paid.</response>
    [HttpPut]
    [Route("")]
    [ResourceAuthorizer("debt:update")]
    public async Task<IActionResult> forgiveADebt([Required] string studentId, [Required] [FromQuery] int tariffId, [Required] [FromQuery] string authorizationToken)
    {
        if (!authorizationToken.Equals("36987"))
        {
            throw new ForbiddenException("Incorrect authorization code.");
        }

        if (tariffId < 1)
        {
            throw new IncorrectDataException("TariffId", "Tariff id must be not 0 or less.");
        }
        
        return Ok(await controller.forgiveADebt(studentId, tariffId));
    }
}