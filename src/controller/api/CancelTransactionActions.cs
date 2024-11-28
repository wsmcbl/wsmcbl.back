using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.accounting;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin", "cashier")]
[Route("accounting")]
[ApiController]
public class CancelTransactionActions(ICancelTransactionController controller) : ActionsBase
{
    
    /// <summary>
    ///  Returns the list of all transactions.
    /// </summary>
    /// <response code="200">Returns existing resources (can be empty list).</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("transactions")]
    public async Task<ActionResult> getTransactionList()
    {
        var result = await controller.getTransactionList();
        return Ok(result.mapToTransactionListDto());
    }
    
    
    /// <summary>
    ///  Cancel transaction by id
    /// </summary>
    /// <response code="200">Returns the edited resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpPut]
    [Route("transactions/{transactionId}")]
    public async Task<ActionResult> cancelTransaction([Required] string transactionId)
    {
        return Ok(await controller.cancelTransaction(transactionId));
    }
}