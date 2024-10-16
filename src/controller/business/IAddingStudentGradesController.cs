using wsmcbl.src.model.academy;

namespace wsmcbl.src.controller.business;

public interface IAddingStudentGradesController
{
    public Task<List<EnrollmentEntity>> getEnrollmentListByTeacherId(string teacherId);
    public Task<List<SubjectEntity>> getEnrollmentByTeacher(string teacherId, string enrollmentId);
    public Task addGrades(string teacherId, List<GradeEntity> grades);
}