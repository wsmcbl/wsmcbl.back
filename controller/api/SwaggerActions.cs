using Microsoft.AspNetCore.Mvc;
using wsmcbl.back.config;

namespace wsmcbl.back.controller.api;

[Route("documentation")]
[ApiController]
public class SwaggerActions : ControllerBase
{
    private readonly string _jsonFilePath;

    public SwaggerActions()
    {
        _jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), ".", "swagger.json");
    }

    [HttpGet("swagger")]
    public async Task<IActionResult> GetJsonData()
    {
        TerminalHelper.ExecuteCommand("rm swagger.json || true");
        TerminalHelper.ExecuteCommand("wget --no-check-certificate https://localhost:7211/swagger/v1/swagger.json || true");
        
        if (!System.IO.File.Exists(_jsonFilePath))
        {
            return NotFound();
        }

        var jsonData = await System.IO.File.ReadAllTextAsync(_jsonFilePath);
        return Content(jsonData, "application/json");
    }
}