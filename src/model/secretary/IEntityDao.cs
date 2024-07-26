using wsmcbl.src.model.dao;

namespace wsmcbl.src.model.secretary;

public interface IGradeDao : IGenericDao<GradeEntity, string>
{
    public void createList(List<GradeEntity> gradeList);
    public Task<List<GradeEntity>> getAllForTheCurrentSchoolyear();
}

public interface ISchoolyearDao : IGenericDao<SchoolYearEntity, string>
{
    public Task<SchoolYearEntity> getNewSchoolYear();
    public Task<SchoolYearEntity> getSchoolYearByLabel(int year);
}

public interface IStudentDao : IGenericDao<StudentEntity, string>
{
    public Task<List<StudentEntity>> getAllWithSolvency();
    public Task updateAsync(StudentEntity entity);
}

public interface IStudentFileDao : IGenericDao<StudentFileEntity, string>,
    IStudentElement<StudentFileEntity>;
public interface IStudentTutorDao : IGenericDao<StudentTutorEntity, string>, 
    IStudentElement<StudentTutorEntity>;
public interface IStudentParentDao : IGenericDao<StudentParentEntity, string>,
    IStudentElement<StudentParentEntity>;
public interface IStudentMeasurementsDao 
    : IGenericDao<StudentMeasurementsEntity, string>, IStudentElement<StudentMeasurementsEntity>;


public interface IGradeDataDao : IGenericDao<GradeDataEntity, string>;
public interface ISubjectDataDao : IGenericDao<SubjectDataEntity, string>;
public interface ITariffDataDao : IGenericDao<TariffDataEntity, string>;



public interface IStudentElement<T> 
{
    public Task updateAsync(T entity);
}
