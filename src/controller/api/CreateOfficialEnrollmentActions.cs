using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.input;
using wsmcbl.src.exception;
using DtoMapper = wsmcbl.src.dto.output.DtoMapper;

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
        return Ok(DtoMapper.mapListToDto(grades));
    }
    
    [HttpPost]
    [Route("grades")]
    public async Task<IActionResult> createGrade(GradeDto grade)
    {
        await controller.createGrade(grade.toEntity());
        return Ok();
    }

    [HttpPut]
    [Route("grades")]
    public async Task<IActionResult> updateGrade(GradeDto grade)
    {
        try
        {
            await controller.updateGrade(grade.toEntity());
            return Ok();
        }
        catch (EntityNotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("grades/subjects")]
    public async Task<IActionResult> getSubjectList()
    {
        var subjects = await controller.getSubjectList();
        return Ok(subjects);
    }

    [HttpPut]
    [Route("grades/subjects")]
    public async Task<IActionResult> updateSubjects(SubjectsToUpdateDto dto)
    {
        var list = dto.getSubjectList();
        var gradeId = dto.getGradeId();

        try
        {
            await controller.updateSubjects(gradeId, list);
            return Ok();
        }
        catch (EntityNotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("enrollments")]
    public async Task<IActionResult> getEnrollmentList()
    {
        var list = await controller.getEnrollmentList();
        return Ok(list);
    }

    [HttpGet]
    [Route("enrollments/{enrollmentId}")]
    public async Task<IActionResult> getEnrollment([Required] string enrollmentId)
    {
        try
        {
            var enrollment = await controller.getEnrollment(enrollmentId);
            return Ok(enrollment);
        }
        catch (EntityNotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("enrollments")]
    public async Task<IActionResult> updateEnrollment(EnrollmentDto enrollment)
    {
        await controller.updateEnrollment(enrollment.toEntity());
        return Ok();
    }

    [HttpPut]
    [Route("enrollments/subjects")]
    public async Task<IActionResult> assignTeacher(TeacherToAssignDto dto)
    {
        await controller.assignTeacher(dto.getEnrollmentId(), dto.getSubjectId(), dto.getTeacher());
        return Ok();
    }
}