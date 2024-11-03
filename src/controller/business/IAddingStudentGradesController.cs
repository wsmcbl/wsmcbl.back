using wsmcbl.src.model.academy;

namespace wsmcbl.src.controller.business;

public interface IAddingStudentGradesController
{
    public Task<List<PartialEntity>> getPartialList();
    public Task<EnrollmentEntity> getEnrollmentById(string enrollmentId);
    public Task<List<EnrollmentEntity>> getEnrollmentListByTeacherId(string teacherId);
    public Task<List<SubjectEntity>> getSubjectList(string enrollmentId, string teacherId);
    public Task addGrades(string teacherId, List<GradeEntity> grades);
}