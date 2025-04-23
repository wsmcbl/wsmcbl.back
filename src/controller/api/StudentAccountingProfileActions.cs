using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.accounting;
using wsmcbl.src.exception;
using wsmcbl.src.middleware;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.api;

[Route("accounting/students")]
[ApiController]
public class StudentAccountingProfileActions(DaoFactory daoFactory) : ActionsBase
{
    /// <summary>Update student discount.</summary>
    /// <response code="200">If the resource was edited correctly.</response>
    /// <response code="400">Parameter is not valid.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource not found.</response>
    [HttpPut]
    [Route("")]
    [Authorizer("student:update")]
    public async Task<IActionResult> updateDiscount(ChangeStudentDiscountDto dto)
    {
        if (!dto.authorizationToken.Equals("36987"))
        {
            throw new ForbiddenException("Incorrect authorization code.");
        }

        var controller = new UpdateStudentProfileController(daoFactory);
        await controller.updateStudentDiscount(dto.studentId, dto.discountId);
        return Ok();
    }
    
    /// <summary>Returns account statement by student id in PDF format.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="500">Error creating document.</response>
    [HttpGet]
    [Route("{studentId}/account-statement/export")]
    [Authorizer("student:read")]
    public async Task<IActionResult> getAccountStatementDocument([Required] string studentId)
    {
        var controller = new PrintDocumentByStudentController(daoFactory);
        
        var result = await controller.getAccountStatementDocument(studentId, getAuthenticatedUserId());
        return File(result, getContentType(1), $"{studentId}.account-statement.pdf");
    }
}