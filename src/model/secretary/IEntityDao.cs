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

public interface ISchoolyearDao : IGenericDao<SchoolYearEntity, string>
{
    public Task<SchoolYearEntity> getByLabel(int year);
    public Task<SchoolYearEntity> getCurrent(bool withProperties = true);
    public Task<SchoolYearEntity> getCurrentOrNew();
    public Task<SchoolYearEntity> getNewOrCurrent();
    public Task<SchoolYearEntity> getOrCreateNew();
}

public interface IStudentDao : IGenericDao<StudentEntity, string>, IStudentElement<StudentEntity>
{
    public Task<StudentEntity> getFullById(string id);
    public Task<StudentEntity?> findDuplicateOrNull(StudentEntity student);
    public Task<PagedResult<StudentView>> getStudentViewList(StudentPagedRequest request);
    public Task<PagedResult<StudentRecordView>> getStudentRecordViewList(StudentPagedRequest request);
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
