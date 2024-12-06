using wsmcbl.src.model.academy;

namespace wsmcbl.src.controller.business;

public interface IMoveStudentFromEnrollmentController
{
    public Task<StudentEntity> changeStudentEnrollment(StudentEntity studentValue, EnrollmentEntity enrollment);
    public Task<StudentEntity> getStudentOrFailed(string studentId);
    public Task<EnrollmentEntity> getEnrollmentOrFailed(string enrollmentId, string oldEnrollmentId);
    public Task<bool> isThereAnActivePartial();
}