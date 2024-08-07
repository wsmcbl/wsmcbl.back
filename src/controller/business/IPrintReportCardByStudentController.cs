using wsmcbl.src.model.academy;

namespace wsmcbl.src.controller.business;

public interface IPrintReportCardByStudentController
{
    public Task<StudentEntity> getStudentScoreInformation(string studentId);
    Task<TeacherEntity> getTeacherByEnrollment(string enrollmentId);
    public Task<byte[]> getReportCard(string studentId, (int lateArrivals, int justifications, int unjustifications) data);
}