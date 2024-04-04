using Microsoft.EntityFrameworkCore;
using wsmcbl.back.model.accounting;

namespace wsmcbl.back.database;

public class StudentDaoPostgres(PostgresContext context) : GenericDaoPostgres<StudentEntity, string>(context), IStudentDao
{
    public new async Task<StudentEntity?> getById(string id)
    {
        return context.Student
            .Include(e => e.transactions)
            .ThenInclude(t => t.tariffs)
            .FirstOrDefault(e => e.studentId == id);
    }
}

public class TariffDaoPostgres(PostgresContext context) : GenericDaoPostgres<TariffEntity, int>(context), ITariffDao;

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
}
