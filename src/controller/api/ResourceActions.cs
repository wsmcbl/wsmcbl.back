using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.secretary;
using wsmcbl.src.middleware;
using wsmcbl.src.model.config;

namespace wsmcbl.src.controller.api;

[Route("resources")]
[ApiController]
public class ResourceActions(ResourceController controller) : ControllerBase
{
    /// <summary>Returns the media by type and schoolyear.</summary>
    /// <param name="type">The type of the media, the default value is 1.</param>
    /// <param name="schoolyear">The schoolyear of the media, for example, "2024", "2025".</param>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">If resource not exist.</response>
    [HttpGet]
    [Route("medias")]
    public async Task<IActionResult> getMedia([FromQuery] int type, [FromQuery] int schoolyear)
    {
        var result = await controller.getMedia(type, schoolyear);
        return Ok(new {value = result});
    }
    
    /// <summary>Returns the list of all students.</summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    [ResourceAuthorizer("admin")]
    [HttpGet]
    [Route("medias/lists")]
    public async Task<IActionResult> getMediaList()
    {
        return Ok(await controller.getMediaList());
    }
    
    /// <summary>Create a new media resource.</summary>
    /// <response code="201">Returns a new resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource depends on another resource not found.</response>
    [ResourceAuthorizer("admin")]
    [HttpPost]
    [Route("medias")]
    public async Task<IActionResult> createMedia(MediaEntity media)
    {
        media.mediaId = 0;
        var result = await controller.createMedia(media);
        return CreatedAtAction(null, result);
    }
    
    /// <summary>Update media resource.</summary>
    /// <response code="200">Returns the edited resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">If resource not exist.</response>
    [ResourceAuthorizer("admin")]
    [HttpPut]
    [Route("medias")]
    public async Task<IActionResult> updateMedia(MediaEntity media)
    {
        var result = await controller.updateMedia(media);
        return Ok(result);
    }
    
    /// <summary>Returns the tutor list.</summary>
    /// <response code="200">Return list, the list can be empty</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [ResourceAuthorizer("admin")]
    [HttpGet]
    [Route("tutors")]
    public async Task<IActionResult> getTutorList()
    {
        var result = await controller.getTutorList();
        
        return Ok(result);
    }  
}