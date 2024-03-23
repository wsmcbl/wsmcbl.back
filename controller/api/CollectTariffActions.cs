using Microsoft.AspNetCore.Mvc;
using wsmcbl.back.controller.business;
using wsmcbl.back.dto.output;

namespace wsmcbl.back.controller.api;

[Route("accounting")]
[ApiController]
public class CollectTariffActions(ICollectTariffController controller) : ControllerBase
{
    [HttpGet]
    [Route("students")]
    public async Task<IActionResult> getStudentList()
    {
        StudentTransformerDto service = new StudentTransformerDto();
        var students = await controller.getStudentsList();
        return Ok(service.getStudentList(students));
    } 
    
    
    [HttpGet]
    [Route("students/{studentId}")]
    public async Task<IActionResult> getStudentById(string studentId)
    {
        var student = await controller.getStudent(studentId);
        if (student == null)
        {
            return NotFound();
        }

        return Ok(new StudentDtoFull(student));
    }
}