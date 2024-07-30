using wsmcbl.src.controller.service;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class PrintDocumentController(DaoFactory daoFactory) : PDFController
{
    public async Task<byte[]> getEnrollDocument(string studentId)
    {
        var student = await daoFactory.studentDao!.getByIdWithProperties(studentId);
        
        var latexBuilder = new ReportCardLatexBuilder(resource, $"{resource}/out", student);
        latexBuilder.setGrade("Decimo grado");
        setLatexBuilder(latexBuilder);

        return getPDF();
    }
}