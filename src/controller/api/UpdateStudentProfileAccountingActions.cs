using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.accounting;
using wsmcbl.src.exception;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin", "cashier")]
[Route("accounting")]
[ApiController]
public class UpdateStudentProfileAccountingActions(UpdateStudentProfileController controller) : ActionsBase
{
    /// <summary>Update student discount.</summary>
    /// <response code="200">If the resource was edited correctly.</response>
    /// <response code="400">Parameter is not valid.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource not found.</response>
    [HttpPut]
    [Route("students")]
    public async Task<IActionResult> updateDiscount(ChangeStudentDiscountDto dto)
    {
        if (!dto.authorizationToken.Equals("36987"))
        {
            throw new UnauthorizedException("Incorrect authorization code.");
        }
        
        await controller.updateStudentDiscount(dto.studentId, dto.discountId);
        return Ok();
    }
}