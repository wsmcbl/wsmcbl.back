using wsmcbl.src.controller.service;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class PrintDocumentController(DaoFactory daoFactory) : PDFController
{
    public async Task<byte[]> getReportCardByStudent(string studentId)
    {
        var student = await daoFactory.academyStudentDao!.getByIdInCurrentSchoolyear(studentId);
        var teacher = await daoFactory.teacherDao!.getByEnrollmentId(student.enrollmentId!);
        var subjectList = await daoFactory.subjectDao.getByEnrollmentId(student.enrollmentId);

        var partials = await daoFactory.partialDao!.getListByStudentId(studentId);
        student.setPartials(partials);
        
        var latexBuilder = new ReportCardLatexBuilder(resource, $"{resource}/out");
        latexBuilder.setProperties(student, teacher);
        latexBuilder.setSubjectList(subjectList);
        
        setLatexBuilder(latexBuilder);
        
        return getPDF();
    }
    
    public async Task<byte[]> getEnrollDocument(string studentId)
    {
        var student = await daoFactory.studentDao!.getByIdWithProperties(studentId);
        var enrollment = await daoFactory.enrollmentDao!.getByStudentId(student.studentId);
        
        var latexBuilder = new EnrollSheetLatexBuilder(resource, $"{resource}/out", student);
        latexBuilder.setGrade(enrollment.label);
        setLatexBuilder(latexBuilder);

        return getPDF();
    }
}