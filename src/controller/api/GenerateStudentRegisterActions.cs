using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.secretary;
using wsmcbl.src.middleware;
using wsmcbl.src.model;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.api;

[ApiController]
[Route("secretary/students/registers")]
[ResourceAuthorizer("register:read")]
public class GenerateStudentRegisterActions(GenerateStudentRegisterController controller) : ActionsBase
{
    /// <summary>Returns paged student register.</summary>
    /// <remarks>Values for sortBy: studentId, fullName, isActive, tutor, schoolyear and enrollment.</remarks>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> getStudentRegisterList([FromQuery] StudentPagedRequest request)
    {
        request.checkSortByValue(["studentId", "fullName", "isActive", "tutor", "schoolyear", "enrollment"]);
        
        var result = await controller.getStudentRegisterList(request);
        
        var pagedResult = new PagedResult<StudentRegisterViewDto>(result.data.mapListToDto());
        pagedResult.setup(result);
        
        return Ok(pagedResult);
    }
    
    /// <summary>Returns the student register document in current schoolyear.</summary>
    /// <response code="200">Return existing resources.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource depends on another resource not found.</response>
    [HttpGet]
    [Route("documents")]
    public async Task<IActionResult> getStudentRegisterDocument()
    {
        var result = await controller.getStudentRegisterDocument(getAuthenticatedUserId());
        return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "student.register.xlsx");
    }
}