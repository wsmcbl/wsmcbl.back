using wsmcbl.src.model.dao;

namespace wsmcbl.src.model.secretary;

public interface IGradeDao : IGenericDao<GradeEntity, int>;
public interface ISchoolyearDao : IGenericDao<SchoolYearEntity, string>;
public interface IStudentDao : IGenericDao<StudentEntity, string>;
public interface ISubjectDao : IGenericDao<SubjectEntity, string>;