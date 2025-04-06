using wsmcbl.src.model.dao;

namespace wsmcbl.src.model.academy;

public interface IEnrollmentDao : IGenericDao<EnrollmentEntity, string>
{
    public Task<EnrollmentEntity> getFullById(string enrollmentId);
    public Task<EnrollmentEntity> getByStudentId(string studentId);
    public Task<List<EnrollmentEntity>> getListByTeacherId(string teacherId);
    public Task createRange(ICollection<EnrollmentEntity> enrollmentList);
}

public interface ISubjectDao : IGenericDao<SubjectEntity, string>
{
    public Task<List<SubjectEntity>> getListByTeacherId(string teacherId);
    public Task<List<SubjectEntity>> getByEnrollmentId(string enrollmentId);
    public Task<List<SubjectEntity>> getByEnrollmentId(string enrollmentId, int partial);
    public Task<SubjectEntity?> getBySubjectIdAndEnrollmentId(string subjectId, string enrollmentId);
}

public interface ISubjectPartialDao : IGenericDao<SubjectPartialEntity, int>
{
    public Task<List<SubjectPartialEntity>> getListBySubject(SubjectPartialEntity subjectPartial);
    public Task<List<int>> getIdListBySubject(SubjectPartialEntity subjectPartial);
    public Task<List<SubjectPartialEntity>> getListByPartialIdAndEnrollmentId(int partialId, string enrollmentId);
}

public interface ITeacherDao : IGenericDao<TeacherEntity, string>
{
    public Task<TeacherEntity?> getByEnrollmentId(string enrollmentId);
    public Task<TeacherEntity> getByUserId(Guid userId);
    public Task<List<TeacherEntity>> getListWithSubjectGradedForCurrentPartial();
    public Task<string> getCurrentEnrollmentId(string teacherId);
}

public interface IStudentDao : IGenericDao<StudentEntity, string>
{
    public Task<bool> isEnrolled(string studentId);
    public Task update(string studentId, string enrollmentId);
    public Task<StudentEntity> getCurrentById(string studentId);
    public Task<List<StudentEntity>> getListWithGradesForCurrentSchoolyear(string enrollmentId, int partial);
    public Task<List<StudentEntity>> getListBeforeFirstPartial(string? enrollmentId = null);
}

public interface IPartialDao : IGenericDao<PartialEntity, int>
{
    public Task<List<PartialEntity>> getListForCurrentSchoolyear();
    public Task<List<PartialEntity>> getListByEnrollmentId(string enrollmentId);
}

public interface ISemesterDao : IGenericDao<SemesterEntity, int>
{
    public Task<List<SemesterEntity>> getListForCurrentSchoolyear();
}

public interface IGradeDao : IGenericDao<GradeEntity, int>
{
    public Task addRange(SubjectPartialEntity subjectPartial, List<GradeEntity> gradeList);
}

public interface IWithdrawnStudentDao : IGenericDao<WithdrawnStudentEntity, int>
{
    public Task<List<WithdrawnStudentEntity>> getListByEnrollmentId(string enrollmentId,  bool hasBeforeFirstPartial = false);
}