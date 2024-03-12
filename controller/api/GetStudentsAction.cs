using Microsoft.AspNetCore.Mvc;
using wsmcbl.back.controller.business;
using wsmcbl.back.dto.output;

namespace wsmcbl.back.controller.api;

[Route("[controller]")]
[ApiController]
public class GetStudentsAction : ControllerBase
{
    private ICollectTariffController controller;
    public GetStudentsAction(ICollectTariffController controller)
    {
        this.controller = controller;
    }

    [HttpGet]
    [Route("/students")]
    public IActionResult getStudentList()
    {
        StudentTransformerDto service = new StudentTransformerDto();
        return Ok(service.getStudentList(controller.getStudentsList()));
    }   
    
    [HttpPost]
    [Route("/students")]
    public IActionResult setStudent([FromBody]StudentDto student)
    {
        if(student.id != string.Empty)
            return Ok();
        else
        {
            return BadRequest();
        }
    }   
}