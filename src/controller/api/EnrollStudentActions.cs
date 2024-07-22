using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.input;

namespace wsmcbl.src.controller.api;

[Route("secretary/registrations/")]
[ApiController]
public class EnrollStudentActions(IEnrollStudentController controller) : ControllerBase
{
    [HttpGet]
    [Route("students")]
    public async Task<IActionResult> getStudentsList()
    {
        var result = await controller.getStudentList();
        return Ok(result);
    }

    [HttpGet]
    [Route("students/{studentId}")]
    public async Task<IActionResult> getStudentById([Required] string studentId)
    {
        var result = await controller.getStudentById(studentId);
        return Ok(result);
    }

    [HttpGet]
    [Route("grades")]
    public async Task<IActionResult> getGradeList()
    {
        var result = await controller.getGradeList();
        return Ok(result);
    }

    [HttpPut]
    [Route("")]
    public async Task<IActionResult> saveEnroll(StudentToEnrollDto dto)
    {
        var result = await controller.saveEnroll(dto.toEntity(), dto.enrollmentId);
        return Ok(result);
    }

    [HttpGet]
    [Route("{studentId}")]
    public async Task<IActionResult> printEnrollDocument([Required] string studentId)
    {
        var result = await controller.printEnrollDocument(studentId);
        return Ok(result);
    }
}