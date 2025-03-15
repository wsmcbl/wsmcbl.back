using wsmcbl.src.model.dao;

namespace wsmcbl.src.model.secretary;

public interface IDegreeDao : IGenericDao<DegreeEntity, string>
{
    public Task<PagedResult<DegreeEntity>> getAll(PagedRequest request);
    public Task createRange(List<DegreeEntity> degreeList);
    public Task<DegreeEntity?> getByEnrollmentId(string enrollmentId);
    public Task<DegreeEntity?> getWithAllPropertiesById(string degreeId);
    public Task<List<DegreeEntity>> getValidListForTheSchoolyear();
    public Task<List<DegreeEntity>> getAll(string schoolyearId, bool withStudentsInEnrollment);
}

public interface ISchoolyearDao : IGenericDao<SchoolyearEntity, string>
{
    public Task<SchoolyearEntity?> getById(string schoolyearId, bool withProperty = false);
    public Task<SchoolyearEntity> getByLabel(int year);
    public Task<SchoolyearEntity> getCurrent();
    public Task<SchoolyearEntity> getCurrentOrNew();
    public Task<SchoolyearEntity> getNewOrCurrent();
    public Task<SchoolyearEntity> getOrCreateNew();
}

public interface IStudentDao : IGenericDao<StudentEntity, string>, IStudentElement<StudentEntity>
{
    public Task<StudentEntity> getFullById(string id);
    public Task<StudentEntity?> findDuplicateOrNull(StudentEntity student);
    public Task<PagedResult<StudentView>> getStudentViewList(StudentPagedRequest request);
    public Task<PagedResult<StudentRegisterView>> getStudentRegisterViewList(StudentPagedRequest request);
    public Task<List<StudentRegisterView>> getStudentRegisterInCurrentSchoolyear();
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

public interface ISubjectDataDao : IGenericDao<SubjectDataEntity, int>;

public interface ITariffDataDao : IGenericDao<TariffDataEntity, string>;



public interface IStudentElement<in T>
{
    public Task updateAsync(T? entity);
}
