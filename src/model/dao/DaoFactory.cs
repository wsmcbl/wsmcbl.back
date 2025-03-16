using Microsoft.EntityFrameworkCore.Storage;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.config;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.model.dao;

public abstract class DaoFactory
{
    public virtual Task execute() => Task.CompletedTask;

    public virtual void Detached<T>(T element) where T : class
    {}

    public virtual async Task<IDbContextTransaction?> GetContextTransaction()
    {
        await Task.CompletedTask;
        return null;
    }

    public virtual ITariffDao? tariffDao => null;
    public virtual ICashierDao? cashierDao => null;
    public virtual ITransactionDao? transactionDao => null;
    public virtual ITariffTypeDao? tariffTypeDao => null;
    public virtual IDebtHistoryDao? debtHistoryDao => null;
    public virtual IExchangeRateDao? exchangeRateDao => null;
    
    
    public virtual IEnrollmentDao? enrollmentDao => null;
    public virtual ITeacherDao? teacherDao => null;
    public virtual ISubjectDao? subjectDao => null;
    public virtual ISemesterDao? semesterDao => null;
    public virtual IPartialDao? partialDao => null;
    public virtual IGradeDao? gradeDao => null;
    public virtual ISubjectPartialDao? subjectPartialDao => null;
    
    
    public virtual IDegreeDao? degreeDao => null;
    public virtual ISchoolyearDao? schoolyearDao => null;
    
    public virtual IDegreeDataDao? degreeDataDao => null;
    public virtual ISubjectDataDao? subjectDataDao => null;
    public virtual ISubjectAreaDao? subjectAreaDao => null;
    public virtual ITariffDataDao? tariffDataDao => null;
    
    
    public virtual secretary.IStudentDao? studentDao => null;
    public virtual academy.IStudentDao? academyStudentDao => null;
    public virtual accounting.IStudentDao? accountingStudentDao => null;


    public virtual IStudentFileDao? studentFileDao => null;
    public virtual IStudentTutorDao? studentTutorDao => null;
    public virtual IStudentParentDao? studentParentDao => null;
    public virtual IStudentMeasurementsDao? studentMeasurementsDao => null;
    
    
    public virtual IUserDao? userDao => null;
    public virtual IMediaDao? mediaDao => null;
    public virtual IPermissionDao? permissionDao => null;
    public virtual IUserPermissionDao? userPermissionDao => null;
    public virtual IRoleDao? roleDao => null;
    public virtual IRolePermissionDao? rolePermissionDao => null;
}