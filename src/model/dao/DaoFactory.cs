using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.model.dao;

public abstract class DaoFactory
{
    public virtual ITariffDao? tariffDao => null;
    public virtual ICashierDao? cashierDao => null;
    public virtual ITransactionDao? transactionDao => null;
    public virtual ITariffTypeDao? tariffTypeDao => null;
    public virtual IDebtHistoryDao? debtHistoryDao => null;
    public virtual IGenericDao<T, string>? studentDao<T>() => null;
    public virtual IGradeDao? gradeDao => null;

    public virtual Task execute() => Task.CompletedTask;
}