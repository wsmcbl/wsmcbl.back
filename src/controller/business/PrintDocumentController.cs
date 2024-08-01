using wsmcbl.src.controller.service;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class PrintDocumentController(DaoFactory daoFactory) : PDFController
{
    public async Task<byte[]> getReportCardByStudent(string studenId)
    {
        var student = await daoFactory.academyStudentDao.getById(studenId);
        
        var latexBuilder = new ReportCardLatexBuilder(resource, $"{resource}/out");
        setLatexBuilder(latexBuilder);
        return getPDF();
    }
    
    public async Task<byte[]> getEnrollDocument(string studentId)
    {
        var grade = "Primer año";
        var student = await daoFactory.studentDao!.getByIdWithProperties(studentId);
        
        var latexBuilder = new EnrollSheetLatexBuilder(resource, $"{resource}/out", student);
        latexBuilder.setGrade(grade);
        setLatexBuilder(latexBuilder);

        return getPDF();
    }
}