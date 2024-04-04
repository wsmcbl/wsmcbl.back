using wsmcbl.back.model.accounting;

namespace wsmcbl.back.model.dao;

public abstract class DaoFactory
{
    private DaoFactory factory = null;

    public void setFactory(DaoFactory factory)
    {
        this.factory = factory;
    }

    public DaoFactory getFactory()
    {
        return factory;
    }

    public virtual IStudentDao studentDao()
    {
        return null;
    }

    public virtual ITariffDao tariffDao()
    {
        return null;
    }

    public virtual ITransactionDao transactionDao()
    {
        return null;
    }
}