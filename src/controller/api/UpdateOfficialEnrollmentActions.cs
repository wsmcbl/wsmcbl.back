using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.secretary;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin","secretary")]
[Route("secretary")]
[ApiController]
public class UpdateOfficialEnrollmentActions(CreateOfficialEnrollmentController controller) : ControllerBase
{
    /// <summary>
    ///  Update official enrollment resource.
    /// </summary>
    /// <response code="200">When update is successful.</response>
    /// <response code="400">The dto in is not valid.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Enrollment or internal record not found.</response>
    [HttpPut]
    [Route("degrees/enrollments/{enrollmentId}")]
    public async Task<IActionResult> updateEnrollment([Required] string enrollmentId, EnrollmentToUpdateDto dto)
    {
        var teacher = await controller.assignTeacherGuide(dto.teacherId, enrollmentId);
        var enrollment = await controller.updateEnrollment(dto.toEntity(enrollmentId));

        return Ok(enrollment.mapToDto(teacher));
    }

    /// <summary>
    /// Returns the search results based on the provided query.
    /// </summary>
    /// <param name="q">The query string in the format "value". Supported values are "all" and "new".</param>
    /// <response code="200">Returns the search results.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="400">If the query parameter is missing or not in the correct format.</response>
    [HttpGet]
    [Route("configurations/schoolyears")]
    public async Task<IActionResult> getSchoolYears([FromQuery] string q)
    {
        if (string.IsNullOrWhiteSpace(q))
        {
            return NotFound("Query parameter 'q' is required.");
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

        return NotFound("Unknown type value.");
    }


    /// <summary>Create new schoolyear.</summary>
    /// <response code="201">If the resource is created.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource depends on another resource not found (degree).</response>
    /// <remarks>
    /// Property semester in partialList can be 1 or 2.
    /// Property exchangeRate can be double.
    /// </remarks>
    [HttpPost]
    [Route("configurations/schoolyears")]
    public async Task<IActionResult> createSchoolYear(SchoolYearToCreateDto dto)
    {
        var result = await controller.createSchoolYear(dto.getGradeList(), dto.getTariffList());
        await controller.createSemester(result, dto.getPartialList());
        await controller.createExchangeRate(result, dto.exchangeRate);

        return CreatedAtAction(null, result);
    }

    /// <summary>Create new subject catalog.</summary>
    /// <response code="201">If the resource is created.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource depends on another resource not found (degree).</response>
    [HttpPost]
    [Route("configurations/schoolyears/subjects")]
    public async Task<IActionResult> createSubject(SubjectDataDto dto)
    {
        var result = await controller.createSubject(dto.toEntity());
        return CreatedAtAction(null, result);
    }

    /// <summary>Create new tariff catalog.</summary>
    /// <response code="201">If the resource is created.</response>
    /// <response code="401">If the query was made without authentication.</response>
    /// <response code="403">If the query was made without proper permissions.</response>
    /// <response code="404">Resource depends on another resource not found (degree).</response>
    [HttpPost]
    [Route("configurations/schoolyears/tariffs")]
    public async Task<IActionResult> createTariff(TariffDataDto dto)
    {
        var result = await controller.createTariff(dto.toEntity());
        return CreatedAtAction(null, result);
    }
}