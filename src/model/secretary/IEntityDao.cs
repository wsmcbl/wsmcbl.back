using wsmcbl.src.model.dao;

namespace wsmcbl.src.model.secretary;

public interface IGradeDao : IGenericDao<GradeEntity, string>
{
    public void createList(List<GradeEntity> gradeList);
}
public interface ISchoolyearDao : IGenericDao<SchoolYearEntity, string>
{
    public Task<SchoolYearEntity> getNewSchoolYear();
}

public interface IStudentDao : IGenericDao<StudentEntity, string>;
public interface ISubjectDao : IGenericDao<SubjectEntity, string>;

public interface IGradeDataDao : IGenericDao<GradeDataEntity, string>;
public interface ISubjectDataDao : IGenericDao<SubjectDataEntity, string>;
public interface ITariffDataDao : IGenericDao<TariffDataEntity, string>;