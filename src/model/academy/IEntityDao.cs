using wsmcbl.src.model.dao;

namespace wsmcbl.src.model.academy;

public interface IEnrollmentDao : IGenericDao<EnrollmentEntity, string>;
public interface ISubjectDao : IGenericDao<SubjectEntity, int>
{
    Task<List<SubjectEntity>> getByEnrollmentId(string enrollmentId);
}

public interface ITeacherDao : IGenericDao<TeacherEntity, string>
{
    public Task<TeacherEntity> getByEnrollmentId(string enrollmentId);
}

public interface IStudentDao : IGenericDao<StudentEntity, string>
{
    public Task<StudentEntity?> getByIdAndSchoolyear(string studentId, string schoolyearId);
}