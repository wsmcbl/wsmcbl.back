using wsmcbl.src.model.academy;

namespace wsmcbl.src.controller.business;

public interface IMoveStudentFromEnrollmentController
{
    public Task<StudentEntity> changeStudentEnrollment(StudentEntity studentValue, string enrollmentId);
    public Task<StudentEntity> getValidStudent(string studentId);
}