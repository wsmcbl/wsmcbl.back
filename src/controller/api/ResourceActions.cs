using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.exception;
using wsmcbl.src.middleware;
using wsmcbl.src.model;
using wsmcbl.src.model.config;
using wsmcbl.src.utilities;

namespace wsmcbl.src.controller.api;

[Route("resources")]
[ApiController]
public class ResourceActions(ResourceController controller) : ControllerBase
{
    /// <summary>Returns the media by type and schoolyear label.</summary>
    /// <param name="type">The type of the media, the default value is 1.</param>
    /// <param name="schoolyear">The schoolyear of the media, for example, "2024", "2025".</param>
    /// <response code="200">Returns a resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">If resource not exist.</response>
    [HttpGet]
    [Route("medias/{schoolyear:int}/{type:int}")]
    public async Task<IActionResult> getMedia([Required] int schoolyear, [Required] int type)
    {
        var result = await controller.getMedia(type, schoolyear);
        return Ok(new {value = result});
    }
    
    /// <summary>Returns the list of all media.</summary>
    /// <response code="200">Returns a list, the list can be empty.</response>
    [HttpGet]
    [Route("medias")]
    [Authorizer("admin")]
    public async Task<IActionResult> getMediaList()
    {
        return Ok(await controller.getMediaList());
    }
    
    /// <summary>Create a new media resource.</summary>
    /// <response code="201">Returns a new resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource depends on another resource not found.</response>
    [HttpPost]
    [Route("medias")]
    [Authorizer("admin")]
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
    [HttpPut]
    [Route("medias")]
    [Authorizer("admin")]
    public async Task<IActionResult> updateMedia(MediaEntity media)
    {
        var result = await controller.updateMedia(media);
        return Ok(result);
    }
    
    /// <summary>Returns the tutor list.</summary>
    /// <response code="200">Return list, the list can be empty</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [Authorizer("admin")]
    [HttpGet]
    [Route("tutors")]
    public async Task<IActionResult> getTutorList()
    {
        var result = await controller.getTutorList();
        
        return Ok(result);
    } 
    
    /// <summary>Returns the transaction invoice view list.</summary>
    /// <remarks> The date values must be "day-month-year" format, example "25-01-2025".</remarks>
    /// <remarks> A date before 2,000 is not accepted.</remarks>
    /// <param name="from">The default time is set to 00:00 hours.</param>
    /// <param name="to">
    /// The default time is set to 23:59.
    /// If the date entered corresponds to the current date,
    /// the time will be adjusted to the time at which the query is made.
    /// </param>
    /// <response code="200">Return list, the list can be empty</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    [HttpGet]
    [Route("transactions/invoices")]
    [Authorizer("admin")]
    public async Task<IActionResult> getTransactionInvoiceViewList([FromQuery] [Required] string from, [FromQuery] string to)
    {
        if (!TransactionReportByDateActions.hasDateFormat(from) || !TransactionReportByDateActions.hasDateFormat(to))
        {
            throw new IncorrectDataException("Some of the dates are not in the correct format.");
        }

        var range = TransactionReportViewPagedRequest.parseToDateTime(from, to);
        
        var result = await controller.getTransactionInvoiceViewList(range.from, range.to);
        result.ForEach(e => e.total = e.total.round());
        
        return Ok(result);
    } 
}