using wsmcbl.back.model.accounting;
using wsmcbl.back.model.secretary;
using IStudentDao = wsmcbl.back.model.accounting.IStudentDao;
using IStudentSecretaryDao = wsmcbl.back.model.secretary.IStudentDao;

namespace wsmcbl.back.model.dao;

public abstract class DaoFactory
{
    private DaoFactory? factory;

    public void setFactory(DaoFactory _factory)
    {
        factory = _factory;
    }

    public DaoFactory? getFactory()
    {
        return factory;
    }

    public virtual ICashierDao? cashierDao()
    {
        return null;
    }

    public virtual IStudentDao? studentDao()
    {
        return null;
    }

    public virtual IStudentSecretaryDao? studentSecretaryDao()
    {
        return null;
    }

    public virtual ITariffDao? tariffDao()
    {
        return null;
    }

    public virtual ITransactionDao? transactionDao()
    {
        return null;
    }

    public virtual IUserDao? userDao()
    {
        return null;
    }
}