using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.middleware;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.api;

[Route("secretary/catalogs")]
[ApiController]
public class CreateSubjectDataActions(CreateSubjectDataController controller) : ControllerBase
{
    /// <summary>Returns subject catalog list.</summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("subjects")]
    [ResourceAuthorizer("catalog:read")]
    public async Task<IActionResult> getSubjectDataList()
    {
        return Ok(await controller.getSubjectDataList());
    }

    /// <summary>Create new subject catalog.</summary>
    /// <remarks>The subjectDataId is not necessary.</remarks>
    /// <response code="201">If the resource is created.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource depends on another resource not found (degree).</response>
    [HttpPost]
    [Route("subjects")]
    [ResourceAuthorizer("catalog:create")]
    public async Task<IActionResult> createSubjectData(SubjectDataEntity value)
    {
        value.subjectDataId = 0;
        var result = await controller.createSubjectData(value);
        return CreatedAtAction(null, result);
    }

    /// <summary>Update subject.</summary>
    /// <remarks>The subjectDataId is not necessary.</remarks>
    /// <response code="200">If the resource is updated.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource not found.</response>
    [HttpPut]
    [Route("subjects/{subjectId:int}")]
    [ResourceAuthorizer("catalog:update")]
    public async Task<IActionResult> updateSubjectData(int subjectId, SubjectDataEntity value)
    {
        value.subjectDataId = subjectId;
        await controller.updateSubjectData(value);
        return Ok();
    }

    /// <summary>Returns degree catalog list.</summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("degrees")]
    [ResourceAuthorizer("catalog:read")]
    public async Task<IActionResult> getDegreeDataList()
    {
        var result = await controller.getDegreeDataList();
        return Ok(result.Select(e => new { e.degreeDataId, e.label, e.tag, e.educationalLevel }));
    }
}