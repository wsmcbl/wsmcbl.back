using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.secretary;

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
    [Route("degrees")]
    public async Task<IActionResult> getDegreeList()
    {
        var result = await controller.getDegreeList();
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
        return File(result, "application/pdf", $"{studentId}.enroll-sheet.pdf");
    }
}