using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.output;
using wsmcbl.src.dto.secretary;

namespace wsmcbl.src.controller.api;

[Route("secretary")]
[ApiController]
public class CreateOfficialEnrollmentActions(ICreateOfficialEnrollmentController controller) : ControllerBase
{
    [HttpGet]
    [Route("teachers")]
    public async Task<IActionResult> getTeacherList()
    {
        var list = await controller.getTeacherList();
        return Ok(list.mapListToBasicDto());
    }
    
    [HttpGet]
    [Route("grades")]
    public async Task<IActionResult> getGradeList()
    {
        var grades = await controller.getGradeList();
        return Ok(grades.mapListToBasicDto());
    }
    
    [HttpGet]
    [Route("grades/{gradeId}")]
    public async Task<IActionResult> getGradeById([Required] string gradeId)
    {
        var grade = await controller.getGradeById(gradeId);
        return Ok(grade!.mapToDto());
    }
    
    [HttpPost]
    [Route("grades/enrollments")]
    public async Task<IActionResult> createEnrollment(EnrollmentToCreateDto dto)
    {
        await controller.createEnrollments(dto.gradeId, dto.quantity);
        return Ok();
    }
    
    [HttpPut]
    [Route("grades/enrollments")]
    public async Task<IActionResult> updateEnrollment(EnrollmentDto dto)
    {
        await controller.updateEnrollment(dto.toEntity());
        return Ok();
    }
    
    /// <param name="q">The query string in the format "value". Supported values are "all" and "new".</param>
    /// <returns>Returns the search results based on the provided query.</returns>
    /// <response code="200">Returns the search results.</response>
    /// <response code="400">If the query parameter is missing or not in the correct format.</response>
    [HttpGet]
    [Route("configurations/schoolyears")]
    public async Task<IActionResult> getSchoolYears([FromQuery] string q)
    {
        if (string.IsNullOrWhiteSpace(q))
        {
            return BadRequest("Query parameter 'q' is required.");
        }

        var value = q.ToLower();

        if (value.Equals("all"))
        {
            var list = await controller.getSchoolYearList();
            return Ok(list.mapListToDto());
        }
        
        if (value.Equals("new"))
        {
            var schoolyearBaseInformation = await controller.getNewSchoolYearInformation();
            return Ok(schoolyearBaseInformation.mapToDto());
        }
        
        return BadRequest("Unknown type value.");
    }
    
    [HttpPost]
    [Route("configurations/schoolyears")]
    public async Task<IActionResult> createSchoolYear(SchoolYearToCreateDto dto)
    {
        await controller.createSchoolYear(dto.getGradeList(), dto.getTariffList());
        return Ok();
    }

    [HttpPost]
    [Route("configurations/schoolyears/subjects")]
    public async Task<IActionResult> createSubject(SubjectDataDto dto)
    {
        var result = await controller.createSubject(dto.toEntity());
        return CreatedAtAction(null, result);
    }

    [HttpPost]
    [Route("configurations/schoolyears/tariffs")]
    public async Task<IActionResult> createTariff(TariffDataDto dto)
    {
        var result = await controller.createTariff(dto.toEntity());
        return CreatedAtAction(null, result);
    }
}