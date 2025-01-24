using wsmcbl.src.model.dao;

namespace wsmcbl.src.model.secretary;

public interface IDegreeDao : IGenericDao<DegreeEntity, string>
{
    public Task createRange(List<DegreeEntity> degreeList);
    public Task<DegreeEntity?> getByEnrollmentId(string enrollmentId);
    public Task<DegreeEntity?> getWithAllPropertiesById(string degreeId);
    public Task<List<DegreeEntity>> getValidListForTheSchoolyear();
    public Task<List<DegreeEntity>> getAll(string schoolyearId, bool withStudentsInEnrollment);
}

public interface ISchoolyearDao : IGenericDao<SchoolYearEntity, string>
{
    public Task<SchoolYearEntity> getByLabel(int year);
    public Task<SchoolYearEntity> getCurrentSchoolyear(bool withProperties = true);
    public Task<SchoolYearEntity> getCurrentOrNewSchoolyear();
    public Task<SchoolYearEntity> getNewOrCurrentSchoolyear();
    public Task<SchoolYearEntity> getOrCreateNewSchoolyear();
}

public interface IStudentDao : IGenericDao<StudentEntity, string>, IStudentElement<StudentEntity>
{
    public Task<List<StudentEntity>> getAllWithRegistrationTariffPaid();
    public Task<StudentEntity> getByIdWithProperties(string id);
    Task<StudentEntity?> getByInformation(StudentEntity student);
    Task<List<(StudentEntity student, string schoolyear, string enrollment)>> getListWhitSchoolyearAndEnrollment();
}

public interface IStudentFileDao : IGenericDao<StudentFileEntity, int>, IStudentElement<StudentFileEntity>;

public interface IStudentTutorDao : IGenericDao<StudentTutorEntity, string>, IStudentElement<StudentTutorEntity>
{
    public Task<StudentTutorEntity?> getByInformation(StudentTutorEntity tutor);
    public Task<bool> hasOnlyOneStudent(string tutorId);
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
