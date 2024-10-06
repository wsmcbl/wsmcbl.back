using wsmcbl.src.controller.service;
using wsmcbl.src.exception;

namespace wsmcbl.src.controller.business;

public class PdfController
{
    protected readonly string resource;
    private LatexBuilder? latexBuilder;

    protected PdfController()
    {
        resource = Environment.GetEnvironmentVariable("Resource_Directory")!;
    }

    protected void setLatexBuilder(LatexBuilder? _latexBuilder)
    {
        latexBuilder = _latexBuilder;
    }

    protected byte[] getPDF()
    {
        if (latexBuilder == null)
        {
            throw new InternalException("LatexBuilder object must not be null.");
        }
        
        latexBuilder!.build();
        
        var pdfBuilder = new PdfBuilder(latexBuilder);
        
        return pdfBuilder.build().getPdf();
    }
}