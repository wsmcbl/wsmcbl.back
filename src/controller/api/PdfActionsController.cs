using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.service;

namespace wsmcbl.src.controller.api;

[ApiController]
[Route("api/[controller]")]
public class PdfActionsController : ControllerBase
{
    private readonly TemplateManager _templateManager;

    public PdfActionsController(TemplateManager templateManager)
    {
        _templateManager = templateManager;
    }

    [HttpGet("{templateName}")]
    public IActionResult GetPdf(string templateName, [FromQuery] Dictionary<string, string> data)
    {
        var template = _templateManager.UpdateTemplate(templateName, data);
        var pdfPath = _templateManager.CompileToPdf(template, templateName);

        var pdfBytes = System.IO.File.ReadAllBytes(pdfPath);
        return File(pdfBytes, "application/pdf", $"{templateName}.pdf");
    }
}