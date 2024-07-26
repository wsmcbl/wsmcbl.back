using wsmcbl.src.controller.service;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class PrintDocumentsController : BaseController, IPrintDocumentsController
{
    private readonly LatexCompiler _latexCompiler;
    
    public PrintDocumentsController(DaoFactory daoFactory) : base(daoFactory)
    {
        _latexCompiler = new LatexCompiler(
            Path.Combine(AppContext.BaseDirectory,AppContext.BaseDirectory, "..","..", "..", "resource"));
    }

    public async Task getEnrollDocument(string studentId, MemoryStream stream)
    {
        var result = await daoFactory.studentDao.getById("2024-0001-kjtc");
        _latexCompiler.CompileLatexToPdf($"{studentId}.tex", stream);
    }
}