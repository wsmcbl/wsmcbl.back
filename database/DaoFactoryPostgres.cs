using wsmcbl.back.model.accounting;
using wsmcbl.back.model.dao;

namespace wsmcbl.back.database;

public class DaoFactoryPostgres(PostgresContext context) : DaoFactory
{
    private ICashierDao? _cashierDao;
    public override ICashierDao cashierDao => _cashierDao ??= new CashierDaoPostgres(context);

    private ITariffDao? _tariffDao;
    public override ITariffDao tariffDao => _tariffDao ??= new TariffDaoPostgres(context);

    private ITransactionDao? _transactionDao;
    public override ITransactionDao transactionDao => _transactionDao ??= new TransactionDaoPostgres(context);

    private IUserDao? _userDao;
    public override IUserDao userDao => _userDao ??= new UserDaoPostgres(context);




    private StudentDaoPostgres? _accountingStudentDao;
    private SecretaryStudentDaoPostgres? _secretaryStudentDao;

    public override IGenericDao<T, string>? studentDao<T>()
    {
        if (typeof(T) == typeof(StudentEntity))
        {
            return (IGenericDao<T, string>?)(_accountingStudentDao ??= new StudentDaoPostgres(context));
        }

        if (typeof(T) == typeof(model.secretary.StudentEntity))
        {
            return (IGenericDao<T, string>?)(_secretaryStudentDao ??= new SecretaryStudentDaoPostgres(context));
        }

        return null;
    }
    
    

    private ITariffTypeDao? _tariffTypeDao;
    public override ITariffTypeDao? tariffTypeDao => _tariffTypeDao ??= new TariffTypeDaoPostgres(context);
}