using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.input;
using wsmcbl.src.dto.output;
using wsmcbl.src.exception;
using DtoMapper = wsmcbl.src.dto.input.DtoMapper;
using StudentDto = wsmcbl.src.dto.input.StudentDto;

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
        return Ok(grades.mapListToDto());
    }

    [HttpPut]
    [Route("grades")]
    public async Task<IActionResult> updateGrade(GradeDto gradeDto)
    {
        try
        {
            await controller.updateGrade(gradeDto.toEntity());
            return Ok();
        }
        catch (EntityNotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("grades/subjects")]
    public async Task<IActionResult> getSubjectListByGrade()
    {
        var subjects = await controller.getSubjectListByGrade();
        return Ok(subjects);
    }

    [HttpPut]
    [Route("grades/subjects")]
    public async Task<IActionResult> updateSubjects(SubjectsToUpdateDto dto)
    {
        try
        {
            await controller.updateSubjects(dto.gradeId, dto.subjectIdsList);
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
    [Route("enrollments/teachers")]
    public async Task<IActionResult> assignTeacher(TeacherToAssignDto dto)
    {
        await controller.assignTeacher(dto.teacherId, dto.subjectId, dto.enrollmentId);
        return Ok();
    }


    [HttpGet]
    [Route("configurations/schoolyears")]
    public async Task<IActionResult> getAllSchoolYears([FromQuery] string q)
    {
        if (string.IsNullOrWhiteSpace(q))
        {
            return BadRequest("Query parameter 'q' is required.");
        }

        var value = q.ToLower();

        if (value.Equals("all"))
        {
            var list = await controller.getSchoolYearList();
            return Ok(SecretaryDtoMapper.mapToDto(list));
        }
        
        if (value.Equals("new"))
        {
            var schoolyearBaseInformation = await controller.getNewSchoolYearInformation();
            return Ok(schoolyearBaseInformation.mapToDto());
        }

        
        return BadRequest("Unknown search value.");
    }
    
    [HttpPost]
    [Route("configurations/schoolyears")]
    public async Task<IActionResult> createSchoolYear(SchoolYearToCreateDto dto)
    {
        await controller.createGrade(dto.toEntity());
        await controller.createSubject(dto.getTariffList());
        return Ok();
    }
}