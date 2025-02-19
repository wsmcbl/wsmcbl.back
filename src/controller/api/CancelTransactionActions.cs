using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.accounting;
using wsmcbl.src.middleware;
using wsmcbl.src.model;

namespace wsmcbl.src.controller.api;

[Route("accounting/transactions")]
[ApiController]
public class CancelTransactionActions(CancelTransactionController controller) : ActionsBase
{
    /// <summary>Returns the list of all transactions.</summary>
    /// <response code="200">Returns existing resources (can be empty list).</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("")]
    [ResourceAuthorizer("transaction:read")]
    public async Task<ActionResult> getTransactionList([FromQuery] PagedRequest request)
    {
        var result = await controller.getTransactionList(request);

        var pagedResult = new PagedResult<TransactionToListDto>(result.data.mapToTransactionListDto());
        pagedResult.setup(result);
        
        return Ok(pagedResult);
    }
    
    /// <summary>Cancel transaction by id.</summary>
    /// <response code="200">Returns the edited resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpPut]
    [Route("{transactionId}")]
    [ResourceAuthorizer("transaction:update")]
    public async Task<ActionResult> cancelTransaction([Required] string transactionId)
    {
        return Ok(await controller.cancelTransaction(transactionId));
    }
}