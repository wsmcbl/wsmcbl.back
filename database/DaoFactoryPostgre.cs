using wsmcbl.back.model.accounting;
using wsmcbl.back.model.dao;

namespace wsmcbl.back.database;

public class DaoFactoryPostgre : DaoFactory
{
    private readonly PostgresContext context;

    public DaoFactoryPostgre(PostgresContext context)
    {
        this.context = context;
    }

    private ICashierDao _cashierDao;
    public override ICashierDao cashierDao()
    {
        return _cashierDao != null ? _cashierDao : 
            _cashierDao = new CashierDaoPostgres(context);
    }

    private IStudentDao _studentDao;
    public override IStudentDao studentDao()
    {
        return _studentDao != null ? _studentDao : 
            _studentDao = new StudentDaoPostgres(context);
    }

    private ITariffDao _tariffDao;
    public override ITariffDao tariffDao()
    {
        return _tariffDao != null ? _tariffDao : 
            _tariffDao = new TariffDaoPostgres(context);
    }

    private ITransactionDao _transactionDao;

    public override ITransactionDao transactionDao()
    {
        return _transactionDao != null ? _transactionDao : _transactionDao = new TransactionDaoPostgres(context);
    }
}