using wsmcbl.src.controller.service;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class PrintDocumentsController : BaseController, IPrintDocumentsController
{
    private readonly string resource;
    public PrintDocumentsController(DaoFactory daoFactory) : base(daoFactory)
    {
        resource = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "resource");
    }

    public async Task<byte[]> getEnrollDocument(string studentId)
    {
        var student = await daoFactory.studentDao!.getByIdWithProperties(studentId);
        
        var latexBuilder = new ReportCardLatexBuilder(resource, $"{resource}/out", student);
        latexBuilder.build();
        
        var pdfBuilder = new PDFBuilder(latexBuilder);
        
        return pdfBuilder.build().getPdf();
    }
}