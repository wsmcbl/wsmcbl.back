using wsmcbl.src.model.academy;

namespace wsmcbl.src.controller.business;

public interface IMoveTeacherGuideFromEnrollmentController
{
    public Task<List<TeacherEntity>> getTeacherList();
    public Task<EnrollmentEntity> getEnrollmentById(string enrollmentId);
    public Task<TeacherEntity> getTeacherById(string teacherId);
    public Task assignTeacherGuide(string newTeacherId, string enrollmentId);
}