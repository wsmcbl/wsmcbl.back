using wsmcbl.src.model.dao;

namespace wsmcbl.src.model.academy;

public interface IEnrollmentDao : IGenericDao<EnrollmentEntity, string>;
public interface IScoreDao : IGenericDao<ScoreEntity, string>;
public interface IStudentDao : IGenericDao<StudentEntity, string>;
public interface ISubjectDao : IGenericDao<SubjectEntity, string>;
public interface ITeacherDao : IGenericDao<TeacherEntity, string>;