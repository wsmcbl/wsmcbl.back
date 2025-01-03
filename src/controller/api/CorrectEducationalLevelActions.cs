using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin", "secretary","cashier")]
[Route("secretary")]
[ApiController]
public class CorrectEducationalLevelActions(CorrectEducationalLevelController controller) : ActionsBase
{
    /// <summary>
    /// Change educational level of the student
    /// </summary>
    /// <response code="200">Returns the edited resource.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">If resource not exist(student or tariff).</response>
    /// <response code="409">If the student has the same level.</response>
    [HttpPut]
    [Route("students/levels")]
    public async Task<IActionResult> forgiveADebt([FromQuery] string studentId, [FromQuery] int level)
    {
        await controller.changeEducationalLevel(studentId, level);
        return Ok();
    }
}