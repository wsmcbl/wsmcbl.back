using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.service;

namespace wsmcbl.src.controller.api;

[ApiController]
[Route("api/[controller]")]
public class PdfController : ControllerBase
{
    private readonly TemplateProcessor _templateProcessor;
    private readonly PdfGenerator _pdfGenerator;

    public PdfController(TemplateProcessor templateProcessor, PdfGenerator pdfGenerator)
    {
        _templateProcessor = templateProcessor;
        _pdfGenerator = pdfGenerator;
    }

    [HttpPost("generate")]
    public IActionResult GeneratePdf([FromBody] Dictionary<string, string> values)
    {
        string templateName = "report-card";
        string processedTexPath = _templateProcessor.ProcessTemplate(templateName, values);
        _pdfGenerator.GeneratePdf(processedTexPath);

        string pdfPath = Path.ChangeExtension(processedTexPath, ".pdf");
        byte[] pdfBytes = System.IO.File.ReadAllBytes(pdfPath);

        return File(pdfBytes, "application/pdf", "output.pdf");
    }
}
