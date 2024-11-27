using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.middleware;
using wsmcbl.src.model.accounting;

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
        return Ok(result);
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
        var result = await controller.cancelTransaction(transactionId);
        return Ok(result);
    }
}