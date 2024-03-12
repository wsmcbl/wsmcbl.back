using Microsoft.AspNetCore.Mvc;
using wsmcbl.back.dto.output;

namespace wsmcbl.back.controller;

[Route("api/[controller]")]
[ApiController]
public class GetStudentsAction : ControllerBase
{
    [HttpGet]
    public List<StudentDto> getStudentList()
    {
        List<StudentDto> students = new List<StudentDto>();

        students.Add(new StudentDto("1", "Kenny Tinoco", "7A"));
        students.Add(new StudentDto("2", "Ezequielito", "6A"));

        return students.ToList();
    }    
}