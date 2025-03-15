using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.secretary;
using wsmcbl.src.middleware;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.api;

[Route("secretary/catalogs/subjects")]
[ApiController]
[ResourceAuthorizer("schoolyear:read")]
public class CreateSubjectDataActions(CreateSubjectDataController controller) : ControllerBase
{
    /// <summary>Returns subject catalog.</summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> getSubjectDataList()
    {
        return Ok(await controller.getSubjectDataList());
    }

    /// <summary>Create new subject catalog.</summary>
    /// <response code="201">If the resource is created.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource depends on another resource not found (degree).</response>
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> createSubjectData(SubjectDataEntity value)
    {
        var result = await controller.createSubjectData(value);
        return CreatedAtAction(null, result);
    }

    /// <summary>Update subject.</summary>
    /// <response code="200">If the resource is updated.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource not found.</response>
    [HttpPut]
    [Route("{subjectId:int}")]
    public async Task<IActionResult> updateSubjectData(int subjectId, SubjectDataEntity value)
    {
        value.subjectDataId = subjectId;
        return Ok(await controller.updateSubjectData(value));
    }
}