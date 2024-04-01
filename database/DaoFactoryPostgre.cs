using System.Transactions;
using Microsoft.EntityFrameworkCore;
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

    private IStudentDao _studentDao;
    public override IStudentDao studentDao()
    {
        return _studentDao != null ? _studentDao : 
            _studentDao = new StudentDaoPostgre(context);
    }

    private ITransactionDao _transactionDao;
    public override ITransactionDao transactionDao()
    {
        return _transactionDao != null ? _transactionDao : _transactionDao = new TransactionDaoPostgre(context);
    }
}