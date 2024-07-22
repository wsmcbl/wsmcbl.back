using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.model.dao;

public abstract class DaoFactory
{
    public virtual Task execute() => Task.CompletedTask;

    public virtual void Detached<T>(T element) where T : class
    {}

    public virtual ITariffDao? tariffDao => null;
    public virtual ICashierDao? cashierDao => null;
    public virtual ITransactionDao? transactionDao => null;
    public virtual ITariffTypeDao? tariffTypeDao => null;
    public virtual IDebtHistoryDao? debtHistoryDao => null;
    public virtual secretary.IStudentDao? secretaryStudentDao => null;
    public virtual academy.IStudentDao? academyStudentDao => null;
    public virtual accounting.IStudentDao? studentDao => null;
    public virtual IGradeDao? gradeDao => null;
    public virtual IEnrollmentDao? enrollmentDao => null;
    public virtual ITeacherDao? teacherDao => null;
    public virtual ISchoolyearDao? schoolyearDao => null;
    public virtual IGradeDataDao? gradeDataDao => null;
    public virtual ISubjectDataDao? subjectDataDao => null;
    public virtual ITariffDataDao? tariffDataDao => null;
}