using Microsoft.AspNetCore.Mvc;
using wsmcbl.back.controller.business;
using wsmcbl.back.dto.output;

namespace wsmcbl.back.controller.api;

[Route("v1/accounting")]
[ApiController]
public class GetStudentsAction : ControllerBase
{
    private ICollectTariffController controller;
    public GetStudentsAction(ICollectTariffController controller)
    {
        this.controller = controller;
    }

    [HttpGet]
    [Route("students")]
    public async Task<IActionResult> getStudentList()
    {
        StudentTransformerDto service = new StudentTransformerDto();
        var students = await controller.getStudentsList();
        return Ok(service.getStudentList(students));
    }   
    
    [HttpPost]
    [Route("students")]
    public IActionResult setStudent([FromBody]StudentDto student)
    {
        if(student.studentId != string.Empty)
            return Ok();
        else
        {
            return BadRequest();
        }
    }   
}