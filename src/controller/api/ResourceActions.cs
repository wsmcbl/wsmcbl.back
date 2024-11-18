using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.config;
using wsmcbl.src.dto.secretary;

namespace wsmcbl.src.controller.api;

[Route("secretary")]
[ApiController]
public class ResourceActions(IResourceController controller) : ControllerBase
{
    /// <summary>
    ///  Returns the list of all students.
    /// </summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    [HttpGet]
    [Route("students")]
    public async Task<IActionResult> getStudentsList()
    {
        var result = await controller.getStudentList();
        return Ok(result.mapToListBasicDto());
    }
    
    
    
    /// <summary>
    ///  Create a new media resource.
    /// </summary>
    /// <response code="201">Returns a new resource.</response>
    /// <response code="404">Resource depends on another resource not found.</response>
    [HttpPost]
    [Route("medias")]
    public async Task<IActionResult> createMedia(MediaToCreateDto dto)
    {
        var result = await controller.createMedia(dto.toEntity());
        return CreatedAtAction(null, result.mapToDto());
    }
    
    /// <summary>
    ///  Returns the media by type and schoolyear.
    /// </summary>
    /// <param name="type">The type of the media, the default value is 1.</param>
    /// <param name="schoolyear">The schoolyear of the media, for example, "2024", "2025".</param>
    /// <response code="200">Returns a resource.</response>
    /// <response code="404">If resource not exist.</response>
    [HttpGet]
    [Route("medias")]
    public async Task<IActionResult> getMedia([FromQuery] int type, [FromQuery] int schoolyear)
    {
        var result = await controller.getMedia(type, schoolyear);
        return Ok(new {value = result});
    }
    
    /// <summary>
    ///  Update media resource.
    /// </summary>
    /// <response code="200">Returns the edited resource.</response>
    /// <response code="404">If resource not exist.</response>
    [HttpPut]
    [Route("medias")]
    public async Task<IActionResult> updateMedia(MediaToCreateDto dto)
    {
        var result = await controller.updateMedia(dto.toEntity());
        return Ok(result.mapToDto());
    }
}