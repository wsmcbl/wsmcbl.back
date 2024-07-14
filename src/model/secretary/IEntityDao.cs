using wsmcbl.src.model.dao;

namespace wsmcbl.src.model.secretary;

public interface IStudentDao : IGenericDao<StudentEntity, string>;
public interface ISubjectDao : IGenericDao<SubjectEntity, string>;
public interface IGradeDao : IGenericDao<GradeEntity, string>;