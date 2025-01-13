using wsmcbl.src.model.dao;

namespace wsmcbl.src.model.secretary;

public interface IDegreeDao : IGenericDao<DegreeEntity, string>
{
    public Task createRange(List<DegreeEntity> degreeList);
    public Task<List<DegreeEntity>> getValidListForTheSchoolyear();
    public Task<DegreeEntity?> getByEnrollmentId(string enrollmentId);
    public Task<DegreeEntity?> getWithAllPropertiesById(string degreeId);
}

public interface ISchoolyearDao : IGenericDao<SchoolYearEntity, string>
{
    public Task<SchoolYearEntity> getCurrentSchoolyear();
    public Task<SchoolYearEntity> getOrCreateNewSchoolyear();
    public Task<string> getValidSchoolyearId();
    public Task<(string currentSchoolyear, string newSchoolyear)> getCurrentAndNewSchoolyearIds();
    public Task<SchoolYearEntity> getSchoolYearByLabel(int year);
}

public interface IStudentDao : IGenericDao<StudentEntity, string>, IStudentElement<StudentEntity>
{
    public Task<List<StudentEntity>> getAllWithSolvency();
    public Task<StudentEntity> getByIdWithProperties(string id);
    Task<StudentEntity?> getByInformation(StudentEntity student);
    Task<List<(StudentEntity student, string schoolyear, string enrollment)>> getListWhitSchoolyearAndEnrollment();
}

public interface IStudentFileDao : IGenericDao<StudentFileEntity, int>, IStudentElement<StudentFileEntity>;
public interface IStudentTutorDao : IGenericDao<StudentTutorEntity, string>, IStudentElement<StudentTutorEntity>
{
    Task<StudentTutorEntity?> getByInformation(StudentTutorEntity tutor);
}

public interface IStudentParentDao : IGenericDao<StudentParentEntity, string>, IStudentElement<StudentParentEntity>;
public interface IStudentMeasurementsDao 
    : IGenericDao<StudentMeasurementsEntity, int>, IStudentElement<StudentMeasurementsEntity>;


public interface IDegreeDataDao : IGenericDao<DegreeDataEntity, string>;

public interface ISubjectDataDao : IGenericDao<SubjectDataEntity, string>;

public interface ITariffDataDao : IGenericDao<TariffDataEntity, string>;



public interface IStudentElement<in T>
{
    public Task updateAsync(T? entity);
}
