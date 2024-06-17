using wsmcbl.src.model.accounting;

namespace wsmcbl.src.model.dao;

public abstract class DaoFactory
{
    public virtual ITariffDao? tariffDao => null;
    public virtual ICashierDao? cashierDao => null;
    public virtual ITransactionDao? transactionDao => null;
    
    public virtual IGenericDao<T, string>? studentDao<T>()
    {
        return null;
    }
    
    public virtual ITariffTypeDao? tariffTypeDao => null;
    public virtual IDebtHistoryDao? debtHistoryDao => null;
}