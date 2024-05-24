using wsmcbl.back.model.accounting;
using wsmcbl.back.model.dao;

namespace wsmcbl.back.database;

public class DaoFactoryPostgres(PostgresContext context) : DaoFactory
{
    private ICashierDao? _cashierDao;
    public override ICashierDao cashierDao()
    {
        return _cashierDao ??= new CashierDaoPostgres(context);
    }

    private IStudentDao? _studentDao;
    public override IStudentDao studentDao()
    {
        return _studentDao ??= new StudentDaoPostgres(context);
    }

    private ITariffDao? _tariffDao;
    public override ITariffDao tariffDao()
    {
        return _tariffDao ??= new TariffDaoPostgres(context);
    }

    private ITransactionDao? _transactionDao;
    public override ITransactionDao transactionDao()
    {
        return _transactionDao ??= new TransactionDaoPostgres(context);
    }

    private IUserDao? _userDao;
    public override IUserDao userDao()
    {
        return _userDao ??= new UserDaoPostgres(context);
    }
}