using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.input;

namespace wsmcbl.src.controller.api;

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

    [HttpGet]
    [Route("grades")]
    public async Task<IActionResult> getGradeList()
    {
        var grades = await controller.getGradeList();
        return Ok(grades);
    }
}