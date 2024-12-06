using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.accounting;
using wsmcbl.src.middleware;

namespace wsmcbl.src.controller.api;

[ResourceAuthorizer("admin","secretary","cashier")]
[Route("accounting")]
[ApiController]
public class CreateStudentProfileActions(CreateStudentProfileController controller) : ControllerBase
{
    /// <summary>
    /// Create a new student profile to collect tariff
    /// </summary>
    /// <remarks>
    /// The educationalLevel property can only take the values 1 (Preescolar), 2 (Primaria) and 3 (Secundaria).
    /// The studentId must be null or empty.
    /// </remarks>
    /// <response code="201">Returns the new resource.</response>
    /// <response code="400">If the dto is not valid.</response>
    /// <response code="409">If the student profile already exists.</response>
    [HttpPost]
    [Route("students")]
    public async Task<IActionResult> createStudent(CreateStudentProfileDto dto)
    {
        var result = await controller.createStudent(dto.student.toEntity(), dto.tutor.toEntity());
        await controller.createAccountingStudent(result, dto.educationalLevel);
        
        return CreatedAtAction(null, result.mapToDto(dto.educationalLevel));
    }
}