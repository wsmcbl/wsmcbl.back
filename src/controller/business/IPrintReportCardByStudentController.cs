using wsmcbl.src.model.academy;

namespace wsmcbl.src.controller.business;

public interface IPrintReportCardByStudentController
{
    public Task<StudentEntity> getStudentGradesInformation(string studentId);
    Task<TeacherEntity> getTeacherByEnrollment(string enrollmentId);
    public Task<byte[]> getReportCard(string studentId);
    public Task<bool> getStudentSolvency(string studentId);
}