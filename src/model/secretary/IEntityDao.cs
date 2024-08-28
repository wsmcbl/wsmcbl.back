using wsmcbl.src.model.dao;

namespace wsmcbl.src.model.secretary;

public interface IDegreeDao : IGenericDao<DegreeEntity, string>
{
    public void createList(List<DegreeEntity> gradeList);
    public Task<List<DegreeEntity>> getAllForTheCurrentSchoolyear();
}

public interface ISchoolyearDao : IGenericDao<SchoolYearEntity, string>
{
    public Task<SchoolYearEntity> getCurrentSchoolYear();
    public Task<SchoolYearEntity> getNewSchoolYear();
}

public interface IStudentDao : IGenericDao<StudentEntity, string>, IStudentElement<StudentEntity>
{
    public Task<List<StudentEntity>> getAllWithSolvency();
    public Task<StudentEntity> getByIdWithProperties(string id);
}

public interface IStudentFileDao : IGenericDao<StudentFileEntity, int>,
    IStudentElement<StudentFileEntity>;
public interface IStudentTutorDao : IGenericDao<StudentTutorEntity, string>, 
    IStudentElement<StudentTutorEntity>;
public interface IStudentParentDao : IGenericDao<StudentParentEntity, string>,
    IStudentElement<StudentParentEntity>;
public interface IStudentMeasurementsDao 
    : IGenericDao<StudentMeasurementsEntity, int>, IStudentElement<StudentMeasurementsEntity>;


public interface IDegreeDataDao : IGenericDao<DegreeDataEntity, string>;

public interface ISubjectDataDao : IGenericDao<SubjectDataEntity, string>;

public interface ITariffDataDao : IGenericDao<TariffDataEntity, string>;



public interface IStudentElement<in T>
{
    public Task updateAsync(T entity);
}
