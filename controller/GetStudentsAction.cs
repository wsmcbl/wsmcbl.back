using Microsoft.AspNetCore.Mvc;
using wsmcbl.back.dto.output;

namespace wsmcbl.back.controller;

[Route("api/[controller]")]
[ApiController]
public class GetStudentsAction : ControllerBase
{
    [HttpGet]
    [Route("/students")]
    public IActionResult getStudentList()
    {
        List<StudentDto> students = new List<StudentDto>();

        students.Add(new StudentDto("1", "Kenny Tinoco", "7A"));
        students.Add(new StudentDto("2", "Ezequielito", "6A"));

        return Ok(students.ToList());
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