using wsmcbl.back.model.accounting;

namespace wsmcbl.back.model.dao;

public abstract class DaoFactory
{
    public virtual ITariffDao? tariffDao => null;
    public virtual ICashierDao? cashierDao => null;
    public virtual ITransactionDao? transactionDao => null;
    public virtual IUserDao? userDao => null;
    
    public virtual IGenericDao<T, string>? studentDao<T>()
    {
        return null;
    }
    
    public virtual ITariffTypeDao? tariffTypeDao => null;
    public virtual IDebtHistoryDao? debtHistoryDao => null;
}