using wsmcbl.src.controller.service;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class PrintDocumentsController : BaseController, IPrintDocumentsController
{
    private readonly LatexCompiler _latexCompiler;
    
    public PrintDocumentsController(DaoFactory daoFactory) : base(daoFactory)
    {
        _latexCompiler = new LatexCompiler(Path.Combine(AppContext.BaseDirectory, "Resources"));
    }

    public async Task getEnrollDocument(string studentId, Stream stream)
    {
        var result = await daoFactory.studentDao.getById(studentId);
        _latexCompiler.CompileLatexToPdf($"{studentId}.tex", stream);
    }
}