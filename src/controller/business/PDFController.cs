using wsmcbl.src.controller.service;

namespace wsmcbl.src.controller.business;

public class PDFController
{
    protected readonly string resource;
    private LatexBuilder? latexBuilder;

    protected PDFController()
    {
        resource = Path.Combine(AppContext.BaseDirectory,"..","..","..", "resource");
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
        
        var pdfBuilder = new PDFBuilder(latexBuilder);
        
        return pdfBuilder.build().getPdf();
    }
}