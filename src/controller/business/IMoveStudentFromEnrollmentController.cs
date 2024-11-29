using wsmcbl.src.model.academy;

namespace wsmcbl.src.controller.business;

public interface IMoveStudentFromEnrollmentController
{
    public Task<StudentEntity> changeStudentEnrollment(string studentId, string enrollmentId);
}