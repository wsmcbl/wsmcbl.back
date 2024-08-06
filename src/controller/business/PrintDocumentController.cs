using wsmcbl.src.controller.service;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class PrintDocumentController(DaoFactory daoFactory) : PDFController
{
    public async Task<byte[]> getReportCardByStudent(string studentId)
    {
        var student = await daoFactory.academyStudentDao!.getByIdInCurrentSchoolyear(studentId);
        var teacher = await daoFactory.teacherDao!.getByEnrollmentId(student.enrollmentId!);
        
        var partials = await daoFactory.partialDao!.getListByStudentId(studentId);
        student.setPartials(partials);
        
        var latexBuilder = new ReportCardLatexBuilder(resource, $"{resource}/out");
        latexBuilder.setStudent(student);
        latexBuilder.setTeacher(teacher);
        latexBuilder.setDegree("Primero A");
        latexBuilder.setSubjectsSort(await getSubjectSort(student.enrollmentId!));
        latexBuilder.unjustifications = "3";
        latexBuilder.lateArrivals = "0";
        latexBuilder.justifications = "1";

        var semesterList = await daoFactory.semesterDao!.getAllOfCurrentSchoolyear();
        latexBuilder.setSemesterList(semesterList);
        
        setLatexBuilder(latexBuilder);
        
        return getPDF();
    }

    private async Task<List<(string initials, string subjectId)>> getSubjectSort(string enrollmentId)
    {
        var subjectList = await daoFactory.subjectDao!.getByEnrollmentId(enrollmentId);

        return subjectList.Select(item => (item.getInitials, item.subjectId)).ToList();
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