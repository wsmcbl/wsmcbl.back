using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace wsmcbl.src.controller.api;

public abstract class BaseController
{
    [NonAction]
    protected static OkObjectResult Ok([ActionResultObjectValue] object? value)
    {
        return new OkObjectResult(value);
    }
    
    [NonAction]
    protected static FileContentResult File(
        byte[] fileContents,
        string contentType,
        string? fileDownloadName)
    {
        var fileContentResult = new FileContentResult(fileContents, contentType)
        {
            FileDownloadName = fileDownloadName
        };
        
        return fileContentResult;
    }

}