using wsmcbl.src.controller.service;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class PrintDocumentsController : BaseController, IPrintDocumentsController
{
    public PrintDocumentsController(DaoFactory daoFactory) : base(daoFactory)
    {
    }

    public async Task<byte[]> getEnrollDocument(string studentId)
    {
        var student = await daoFactory.studentDao!.getByIdWithProperties(studentId);
        
        var _latexBuilder = new ReportCardLatexBuilder("", "");
        _latexBuilder.build(student);
        
        var pdfBuilder = new PdfBuilder<StudentEntity>(_latexBuilder);
        
        return pdfBuilder.build().getPdf();
    }
}