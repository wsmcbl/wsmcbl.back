using wsmcbl.src.model.dao;

namespace wsmcbl.src.model.secretary;

public interface IDegreeDao : IGenericDao<DegreeEntity, string>
{
    public Task<PagedResult<DegreeEntity>> getPaginated(PagedRequest request);
    public Task createRange(List<DegreeEntity> degreeList);
    public Task<DegreeEntity?> getByEnrollmentId(string enrollmentId);
    public Task<DegreeEntity?> getFullById(string degreeId);
    public Task<List<DegreeEntity>> getValidListForNewOrCurrentSchoolyear();
    public Task<List<DegreeEntity>> getListForSchoolyearId(string schoolyearId, bool withStudentsInEnrollment = false);
}

public interface ISchoolyearDao : IGenericDao<SchoolyearEntity, string>
{
    public Task<SchoolyearEntity?> getById(string schoolyearId, bool withProperty = false);
    public Task<SchoolyearEntity> getByLabel(int year);
    public Task<SchoolyearEntity> getCurrent();
    public Task<SchoolyearEntity> getCurrentOrNew();
    public Task<SchoolyearEntity> getNewOrCurrent();
    public Task<SchoolyearEntity> createNewOrFail();
}

public interface IStudentDao : IGenericDao<StudentEntity, string>, IStudentElement<StudentEntity>
{
    public Task<StudentEntity> getFullById(string id);
    public Task<StudentEntity?> findDuplicateOrNull(StudentEntity student);
    public Task<PagedResult<StudentView>> getPaginatedStudentView(StudentPagedRequest request);
    public Task<PagedResult<StudentRegisterView>> getPaginatedStudentRegisterView(StudentPagedRequest request);
    public Task<List<StudentRegisterView>> getStudentRegisterListForCurrentSchoolyear();
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

public interface ISubjectAreaDao : IGenericDao<SubjectAreaEntity, int>;

public interface ITariffDataDao : IGenericDao<TariffDataEntity, int>;



public interface IStudentElement<in T>
{
    public Task updateAsync(T? entity);
}
