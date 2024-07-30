using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.service;

namespace wsmcbl.src.controller.api;

[ApiController]
[Route("api")]
public class PdfActionsController : ControllerBase
{
    private readonly TemplateManager _templateManager;
    private readonly ILatexBuilder<object> _latexBuilder;

    public PdfActionsController(TemplateManager templateManager, ILatexBuilder<object> latexBuilder)
    {
        _templateManager = templateManager;
        _latexBuilder = latexBuilder;
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