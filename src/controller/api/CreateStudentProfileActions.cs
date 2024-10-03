using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.api;

[Route("accounting")]
[ApiController]
public class CreateStudentProfileActions(ICreateStudentProfileController controller) : ControllerBase
{
    [HttpPost]
    [Route("student")]
    public async Task<IActionResult> createStudent()
    {
        StudentEntity student = null!;
        StudentTutorEntity tutor = null!;
        var result = await controller.createStudent(student, tutor);
        return Ok(result);
    }
}