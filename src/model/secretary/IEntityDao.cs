using wsmcbl.src.model.dao;

namespace wsmcbl.src.model.secretary;

public interface IDegreeDao : IGenericDao<DegreeEntity, string>
{
    public void createList(List<DegreeEntity> gradeList);
    public Task<List<DegreeEntity>> getAllForTheCurrentSchoolyear();
}

public interface ISchoolyearDao : IGenericDao<SchoolYearEntity, string>
{
    public Task<SchoolYearEntity> getCurrentSchoolyear();
    public Task<SchoolYearEntity?> getNewSchoolyear();
    public Task<SchoolYearEntity> getOrCreateNewSchoolyear();
}

public interface IStudentDao : IGenericDao<StudentEntity, string>, IStudentElement<StudentEntity>
{
    public Task<List<StudentEntity>> getAllWithSolvency();
    public Task<StudentEntity> getByIdWithProperties(string id);
    Task<StudentEntity?> getByInformation(StudentEntity student);
}

public interface IStudentFileDao : IGenericDao<StudentFileEntity, int>,
    IStudentElement<StudentFileEntity>;
public interface IStudentTutorDao : IGenericDao<StudentTutorEntity, string>, 
    IStudentElement<StudentTutorEntity>
{
    Task<StudentTutorEntity?> getByInformation(StudentTutorEntity tutor);
}

public interface IStudentParentDao : IGenericDao<StudentParentEntity, string>,
    IStudentElement<StudentParentEntity>;
public interface IStudentMeasurementsDao 
    : IGenericDao<StudentMeasurementsEntity, int>, IStudentElement<StudentMeasurementsEntity>;


public interface IDegreeDataDao : IGenericDao<DegreeDataEntity, string>;

public interface ISubjectDataDao : IGenericDao<SubjectDataEntity, string>;

public interface ITariffDataDao : IGenericDao<TariffDataEntity, string>;



public interface IStudentElement<in T>
{
    public Task updateAsync(T? entity);
}
