using wsmcbl.src.model.academy;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public interface ICreateOfficialEnrollmentController
{
    public Task<List<StudentEntity>> getStudentList();
    public Task saveStudent(StudentEntity student);
    public Task<List<GradeEntity>> getGradeList();
    public Task createGrade(GradeEntity entity);
    public Task updateGrade(GradeEntity entity);
    public Task updateSubjects(string gradeId, List<SubjectEntity> list);
    public Task<List<SubjectEntity>> getSubjectList();
    public Task updateEnrollment(EnrollmentEntity entity);
    public Task<List<EnrollmentEntity>> getEnrollmentList();
    public Task<EnrollmentEntity> getEnrollment(string enrollmentId);
    public Task assignTeacher(string enrollmentId, string subjectId, TeacherEntity teacher);
}