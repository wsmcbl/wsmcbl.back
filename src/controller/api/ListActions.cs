using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.business;

namespace wsmcbl.src.controller.api;

[Route("secretary")]
[ApiController]
public class ListActions(IListController controller) : ControllerBase
{
    
}