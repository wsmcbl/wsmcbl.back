using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.secretary;
using wsmcbl.src.middleware;
using wsmcbl.src.model;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.api;

[Route("secretary/students/registers")]
[ApiController]
public class GenerateStudentRegisterActions(GenerateStudentRegisterController controller) : ActionsBase
{
    /// <summary>Returns paged student register.</summary>
    /// <remarks>Values for sortBy: studentId, fullName, isActive, tutor, schoolyear and enrollment.</remarks>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("")]
    [ResourceAuthorizer("student:read")]
    public async Task<IActionResult> getStudentRegisterList([FromQuery] StudentPagedRequest request)
    {
        request.checkSortByValue(["studentId", "fullName", "isActive", "tutor", "schoolyear", "enrollment"]);
        
        var result = await controller.getStudentRegisterList(request);
        
        var pagedResult = new PagedResult<BasicStudentDto>(result.data.mapToListBasicDto());
        pagedResult.setup(result);
        
        return Ok(pagedResult);
    }
}