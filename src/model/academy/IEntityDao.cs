using wsmcbl.src.model.dao;

namespace wsmcbl.src.model.academy;

public interface IEnrollmentDao : IGenericDao<EnrollmentEntity, string>
{
    public Task<EnrollmentEntity> getFullById(string enrollmentId);
    public Task<EnrollmentEntity> getByStudentId(string? studentId);
    public Task<List<EnrollmentEntity>> getListByTeacherId(string teacherId);
}

public interface ISubjectDao : IGenericDao<SubjectEntity, string>
{
    public Task<List<SubjectEntity>> getByEnrollmentId(string enrollmentId);
    public Task<SubjectEntity?> getBySubjectAndEnrollment(string subjectId, string enrollmentId);
}

public interface ISubjectPartialDao : IGenericDao<SubjectPartialEntity, int>
{
    public Task<List<SubjectPartialEntity>> getListByTeacherAndEnrollment(SubjectPartialEntity subjectPartial);
}

public interface ITeacherDao : IGenericDao<TeacherEntity, string>
{
    public Task<TeacherEntity?> getByEnrollmentId(string enrollmentId);
    public Task<List<TeacherEntity>> getByListByIdList(List<string> value);
}

public interface IStudentDao : IGenericDao<StudentEntity, string>
{
    public Task<StudentEntity> getByIdInCurrentSchoolyear(string studentId);
    public Task updateEnrollment(string studentId, string enrollmentId);
}

public interface IPartialDao : IGenericDao<PartialEntity, int>
{
    public Task<List<PartialEntity>> getListByCurrentSchoolyear();
    public Task<List<PartialEntity>> getListWithSubjectByEnrollment(string enrollmentId);
}

public interface ISemesterDao : IGenericDao<SemesterEntity, int>
{
    public Task<List<SemesterEntity>> getAllOfCurrentSchoolyear();
}

public interface IGradeDao : IGenericDao<GradeEntity, int>
{
    public Task addRange(SubjectPartialEntity subjectPartial, List<GradeEntity> gradeList);
}