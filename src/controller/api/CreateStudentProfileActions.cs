using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.accounting;

namespace wsmcbl.src.controller.api;

[Route("accounting")]
[ApiController]
public class CreateStudentProfileActions(ICreateStudentProfileController controller) : ControllerBase
{
    /// <summary>
    /// Create a new student profile to collect tariff
    /// </summary>
    /// <response code="200">Returns the new resource.</response>
    /// <response code="400">If the dto is not valid.</response>
    /// <response code="409">If the student profile already exists.</response>
    [HttpPost]
    [Route("student")]
    public async Task<IActionResult> createStudent(CreateStudentProfileDto dto)
    {
        var result = await controller.createStudent(dto.student.toEntity(), dto.tutor.toEntity(), dto.modality);
        return Ok(result.mapToDto(dto.modality));
    }
}