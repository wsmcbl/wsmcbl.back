using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.input;
using wsmcbl.src.dto.output;
using StudentFullDto = wsmcbl.src.dto.input.StudentFullDto;

namespace wsmcbl.src.controller.api;

[Route("secretary/enrollments/")]
[ApiController]
public class EnrollStudentActions(IEnrollStudentController controller) : ControllerBase
{
    [HttpGet]
    [Route("students")]
    public async Task<IActionResult> getStudentsList()
    {
        var result = await controller.getStudentList();
        return Ok(result.mapToListBasicDto());
    }

    [HttpGet]
    [Route("students/{studentId}")]
    public async Task<IActionResult> getStudentById([Required] string studentId)
    {
        var result = await controller.getStudentById(studentId);
        return Ok(result.mapToDto());
    }

    [HttpGet]
    [Route("grades")]
    public async Task<IActionResult> getGradeList()
    {
        var result = await controller.getGradeList();
        return Ok(result.mapToListBasicDto());
    }

    [HttpPut]
    [Route("")]
    public async Task<IActionResult> saveEnroll(EnrollStudentDto dto)
    {
        var result = await controller.saveEnroll(dto.student.toEntity(), dto.enrollmentId);
        return Ok(result.mapToDto());
    }

    [HttpGet]
    [Route("documents/{studentId}")]
    public async Task<IActionResult> getEnrollDocument([Required] string studentId)
    {
        var result = await controller.getEnrollDocument(studentId);
        return Ok(File(result, "a.pdf"));
    }
}