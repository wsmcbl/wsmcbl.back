using Microsoft.EntityFrameworkCore;
using wsmcbl.back.model.accounting;

namespace wsmcbl.back.database;

public class CashierDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<CashierEntity, string>(context), ICashierDao;

public class StudentDaoPostgres(PostgresContext context) 
    : GenericDaoPostgres<StudentEntity, string>(context), IStudentDao
{
    public new Task<StudentEntity?> getById(string id)
    {
        return Task.FromResult(context.Student
            .Include(e => e.transactions)
            .ThenInclude(t => t.tariffs)
            .FirstOrDefault(e => e.studentId == id));
    }
}

public class TariffDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<TariffEntity, int>(context), ITariffDao
{
    public new async Task<List<TariffEntity>> getAll()
    {
        var elements = await base.getAll();
        return elements.OrderBy(e => e.tariffId).ToList();
    }
}

public class TransactionDaoPostgres(PostgresContext context) 
    : GenericDaoPostgres<TransactionEntity, string>(context), ITransactionDao
{
    public override async Task create(TransactionEntity entity)
    {
        foreach (var tariff in entity.tariffs)
        {
            context.Tariff.Attach(tariff);
        }

        await base.create(entity);
    }

    public async Task<TransactionEntity?> GetLastByStudentId(string studentId)
    {
        var transactionEntity = await context.Transaction
            .Where(t => t != null && t.studentId == studentId && t.dateTime.Date == DateTime.Today)
            .FirstOrDefaultAsync();

        return transactionEntity;
    }
    
    public async Task<TransactionEntity?> getLastByStudentId(string studentId)
    {
        var service = new StudentDaoPostgres(context);

        var student = await service.getById(studentId);

        return student?.getLastTransaction();
    }
}
