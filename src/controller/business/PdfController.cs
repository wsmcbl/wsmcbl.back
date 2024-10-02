using wsmcbl.src.controller.service;

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
            throw new ArgumentException("LatexBuilder object must not be null.");
        }
        
        latexBuilder!.build();
        
        var pdfBuilder = new PdfBuilder(latexBuilder);
        
        return pdfBuilder.build().getPdf();
    }
}