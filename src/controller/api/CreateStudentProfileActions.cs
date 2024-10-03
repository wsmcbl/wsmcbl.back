using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;

namespace wsmcbl.src.controller.api;

[Route("accouting")]
[ApiController]
public class CreateStudentProfileActions(ICreateStudentProfileController controller) : ControllerBase
{
    
}