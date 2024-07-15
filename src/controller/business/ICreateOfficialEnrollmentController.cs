using wsmcbl.src.model.academy;
using wsmcbl.src.model.secretary;
using StudentEntity = wsmcbl.src.model.secretary.StudentEntity;
using SubjectEntity = wsmcbl.src.model.secretary.SubjectEntity;

namespace wsmcbl.src.controller.business;

public interface ICreateOfficialEnrollmentController
{
    public Task<List<StudentEntity>> getStudentList();
    public Task saveStudent(StudentEntity student);
    public Task<List<GradeEntity>> getGradeList();
    public Task<int> createGrade(GradeEntity entity, List<string> subjectIdsList);
    public Task updateGrade(GradeEntity entity);
    public Task updateSubjects(int gradeId, List<string> subjectIdsList);
    public Task<List<SubjectEntity>> getSubjectListByGrade();
    public Task updateEnrollment(EnrollmentEntity entity);
    public Task<List<EnrollmentEntity>> getEnrollmentList();
    public Task<EnrollmentEntity> getEnrollment(string enrollmentId);
    public Task assignTeacher(string teacherId, string subjectId, string enrollmentId);
}