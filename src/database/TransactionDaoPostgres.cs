using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;
using StudentEntity = wsmcbl.src.model.academy.StudentEntity;

namespace wsmcbl.src.database;

public class TransactionDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<TransactionEntity, string>(context), ITransactionDao
{
    public override void create(TransactionEntity entity)
    {
        if (!entity.haveValidContent())
        {
            throw new IncorrectDataBadRequestException("Transaction");
        }

        entity.computeTotal();
        base.create(entity);
    }

    public async Task<List<TransactionReportView>> getByRange(DateTime start, DateTime end)
    {
        return await context.Set<TransactionReportView>()
            .Where(e => e.dateTime >= start && e.dateTime <= end)
            .OrderByDescending(e => e.number)
            .ToListAsync();
    }

    public async Task<List<TransactionReportView>> getViewAll()
    {
        return await context.Set<TransactionReportView>()
            .OrderByDescending(e => e.number)
            .ToListAsync();
    }
}