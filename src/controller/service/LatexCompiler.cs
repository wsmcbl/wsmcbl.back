using MigraDocCore.DocumentObjectModel;
using MigraDocCore.Rendering;

namespace wsmcbl.src.controller.service;

public class LatexCompiler
{
    private readonly string _resourcesFolder;

    public LatexCompiler(string resourcesFolder)
    {
        _resourcesFolder = resourcesFolder;
    }
    
    public void CompileLatexToPdf(string latexFileName, Stream outputStream)
    {
        var latexFilePath = Path.Combine(_resourcesFolder, latexFileName);
        var latexContent = File.ReadAllText(latexFilePath);

        // Crear un nuevo documento MigraDoc
        var document = new Document();

        // Agregar el contenido LaTeX al documento
        var section = document.AddSection();
        section.AddParagraph(latexContent);

        // Renderizar el documento a un archivo PDF
        var renderer = new PdfDocumentRenderer(true)
        {
            Document = document
        };
        
        renderer.RenderDocument();
        renderer.PdfDocument.Save(outputStream);
    }

}
