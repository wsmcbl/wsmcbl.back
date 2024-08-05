using wsmcbl.src.controller.service;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class PrintDocumentController(DaoFactory daoFactory) : PDFController
{
    public async Task<byte[]> getReportCardByStudent(string studenId)
    {
        var student = await daoFactory.academyStudentDao!.getByIdInCurrentSchoolyear(studenId);
        var teacher = await daoFactory.teacherDao!.getByEnrollmentId(student.enrollmentId!);

        var partials = await daoFactory.partialDao!.getListByStudentId(studentId);
        student.setPartials(partials);
        
        var latexBuilder = new ReportCardLatexBuilder(resource, $"{resource}/out");
        latexBuilder.setProperties(student, teacher);
        
        setLatexBuilder(latexBuilder);
        
        return getPDF();
    }
    
    public async Task<byte[]> getEnrollDocument(string studentId)
    {
        var student = await daoFactory.studentDao!.getByIdWithProperties(studentId);
        var grade = await daoFactory.enrollmentDao!.getByStudentId(student.studentId);
        
        var latexBuilder = new EnrollSheetLatexBuilder(resource, $"{resource}/out", student);
        latexBuilder.setGrade(grade.label);
        setLatexBuilder(latexBuilder);

        return getPDF();
    }
}