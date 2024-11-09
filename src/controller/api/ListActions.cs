using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.secretary;

namespace wsmcbl.src.controller.api;

[Route("secretary")]
[ApiController]
public class ListActions(IListController controller) : ControllerBase
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
}