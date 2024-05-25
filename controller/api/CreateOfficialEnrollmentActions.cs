using Microsoft.AspNetCore.Mvc;
using wsmcbl.back.controller.business;

namespace wsmcbl.back.controller.api;

[Route("secretary")]
[ApiController]
public class CreateOfficialEnrollmentActions(ICreateOfficialEnrollmentController controller) : ControllerBase
{
    [HttpGet]
    [Route("students")]
    public async Task<IActionResult> getStudentList()
    {
        var students = await controller.getStudentList();
        return Ok(students);
    } 
}