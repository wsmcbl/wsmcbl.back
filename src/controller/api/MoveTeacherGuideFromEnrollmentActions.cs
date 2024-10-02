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
    public async Task<IActionResult> setTeacherGuide()
    {
        string enrollmentId = "a";
        var newTeacherId = "1";
        
        var enrollment = await controller.getEnrollment(enrollmentId);
        var teacher = await controller.getTeacherById(newTeacherId);
        await controller.assignTeacherGuide(newTeacherId, enrollment.enrollmentId!);
        
        return Ok(enrollment.mapToDto(teacher));
    }
}