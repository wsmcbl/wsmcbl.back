using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.academy;
using wsmcbl.src.dto.secretary;

namespace wsmcbl.src.controller.api;

[Route("academy")]
[ApiController]
public class MoveTeacherGuideFromEnrollmentActions(IMoveTeacherGuideFromEnrollmentController controller) : ControllerBase
{
    [HttpGet]
    [Route("enrollments/teachers")]
    public async Task<IActionResult> getTeacherList()
    {
        var list = await controller.getTeacherList();
        return Ok(list.mapListToDto());
    }
    
    [HttpPut]
    [Route("enrollments")]
    public async Task<IActionResult> setTeacherGuide(MoveTeacherGuideDto dto)
    {
        var enrollment = await controller.getEnrollment(dto.enrollmentId);
        var teacher = await controller.getTeacherById(dto.newTeacherId);
        await controller.assignTeacherGuide(dto.newTeacherId, enrollment.enrollmentId!);
        
        return Ok(enrollment.mapToDto(teacher));
    }
}