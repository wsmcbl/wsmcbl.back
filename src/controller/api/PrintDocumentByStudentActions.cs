using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.exception;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[Route("academy/students")]
[ApiController]
public class PrintDocumentByStudentActions(PrintDocumentByStudentController controller) : ActionsBase
{
    /// <summary>Returns report-card by student id in PDF format.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="500">Error creating document.</response>
    [HttpGet]
    [Route("{studentId}/report-card/export")]
    [Authorizer("student:read")]
    public async Task<IActionResult> getReportCard([Required] string studentId, [FromQuery] string? adminToken)
    {
        if (adminToken == null)
        {
            var isSolvency = await controller.isStudentSolvent(studentId);
            if (!isSolvency)
            {
                throw new ConflictException($"Student with id ({studentId}) has no solvency.");
            }
        }
        else
        {
            checkAdminToken(adminToken);
        }

        var result = await controller.getReportCard(studentId, getAuthenticatedUserId());
        return File(result, getContentType(1), $"{studentId}.report-card.pdf");
    }

    private static void checkAdminToken(string adminToken)
    {
        if(adminToken != "5896")
        {
            throw new UnauthorizedException("User unauthorized.");
        }
    }
    
    /// <summary>Returns active-certificate by student id in PDF format.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="500">Error creating document.</response>
    [HttpGet]
    [Route("{studentId}/active-certificate/export")]
    [Authorizer("student:read")]
    public async Task<IActionResult> getActiveCertificateDocument([Required] string studentId)
    {
        var result = await controller.getActiveCertificateDocument(studentId, getAuthenticatedUserId());
        return File(result, getContentType(1), $"{studentId}.active-certificate.pdf");
    }
    
    /// <summary>Returns proforma document in PDF format.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="500">Error creating document.</response>
    [HttpGet]
    [Route("proforma/export")]
    [Authorizer("student:read")]
    public async Task<IActionResult> getProformaDocument(string? studentId, string? degreeId, string? name)
    {
        var userId = getAuthenticatedUserId();
        var result = studentId != null ? await controller.getProformaDocument(studentId, userId) :
            await controller.getProformaDocument(degreeId, name, userId);
        
        return File(result, getContentType(1), "proforma.pdf");
    }
    
    /// <summary>Returns academic report document by schoolyear id in PDF format.</summary>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="500">Error creating document.</response>
    [HttpGet]
    [Route("academic/record/export")]
    [Authorizer("student:read")]
    public async Task<IActionResult> getAcademicRecordDocument(string studentId, string schoolyearId)
    {
        var userId = getAuthenticatedUserId();
        var result = await controller.getAcademicRecordDocument(studentId, schoolyearId, userId);
        return File(result, getContentType(1), $"{studentId}.academic-record.pdf");
    }
}