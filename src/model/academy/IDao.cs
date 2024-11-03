using wsmcbl.src.dto.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.model.academy;

public interface IEnrollmentDao : IGenericDao<EnrollmentEntity, string>
{
    public Task<EnrollmentEntity> getFullById(string enrollmentId);
    public Task<EnrollmentEntity> getByStudentId(string? studentId);
    public Task<List<EnrollmentEntity>> getListByTeacherId(string teacherId);
}

public interface ISubjectDao : IGenericDao<SubjectEntity, int>
{
    public Task<List<SubjectEntity>> getByEnrollmentId(string enrollmentId);
    public Task<List<SubjectEntity>> getSubjectByTeacherAndEnrollment(string teacherId, string enrollmentId);
}

public interface ISubjectPartialDao : IGenericDao<SubjectPartialEntity, int>
{
    public Task<List<SubjectPartialEntity>> getListByTeacherAndEnrollment(string teacherId, string enrollmentId);
}

public interface ITeacherDao : IGenericDao<TeacherEntity, string>
{
    public Task<TeacherEntity?> getByEnrollmentId(string enrollmentId);
}

public interface IStudentDao : IGenericDao<StudentEntity, string>
{
    public Task<StudentEntity> getByIdAndSchoolyear(string studentId, string schoolyearId);
    public Task<StudentEntity> getByIdInCurrentSchoolyear(string studentId);
}

public interface IPartialDao : IGenericDao<PartialEntity, int>
{
    public Task<List<PartialEntity>> getListByCurrentSchoolyear();
    public Task<List<PartialEntity>> getListByStudentId(string studentId);
}

public interface ISemesterDao : IGenericDao<SemesterEntity, int>
{
    public Task<List<SemesterEntity>> getAllOfCurrentSchoolyear();
}

public interface IGradeDao : IGenericDao<GradeEntity, int>
{
    public Task addingStudentGrades(string teacherId, List<GradeEntity> grades);
}