using Microsoft.AspNetCore.Mvc;
using wsmcbl.back.controller.business;
using wsmcbl.back.dto.input;

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

    /// <param name="student"> Value Sex: true-female, false-man</param>
    [HttpPost]
    [Route("students")]
    public async Task saveStudent([FromBody] StudentDto student)
    {
        await controller.saveStudent(student.toEntity());
    }
}