using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.database;

public class TransactionDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<TransactionEntity, string>(context), ITransactionDao
{
    public new async Task<TransactionEntity?> getById(string id)
    {
        var transaction = await entities
            .Where(e => e.transactionId == id)
            .Include(e => e.details)
            .FirstOrDefaultAsync();

        if (transaction == null)
        {
            throw new EntityNotFoundException("transaction", id);
        }

        return transaction;
    }

    public override void create(TransactionEntity entity)
    {
        if (!entity.haveValidContent())
        {
            throw new IncorrectDataBadRequestException("Transaction");
        }

        entity.computeTotal();
        base.create(entity);
    }

    public async Task<List<TransactionReportView>> getByRange(DateTime from, DateTime to)
    {
        return await context.Set<TransactionReportView>()
            .AsNoTracking()
            .Where(e => e.dateTime >= from && e.dateTime <= to)
            .OrderByDescending(e => e.number)
            .ToListAsync();
    }

    public async Task<List<TransactionReportView>> getViewAll()
    {
        return await context.Set<TransactionReportView>()
            .OrderByDescending(e => e.number)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<TransactionInvoiceView>> getTransactionInvoiceViewList(DateTime from, DateTime to)
    {
        var result = await context.Set<TransactionInvoiceView>()
            .AsNoTracking()
            .Where(e => e.dateTime >= from && e.dateTime <= to)
            .ToListAsync();

        foreach (var item in result)
        {
            item.ChangeToUtc6();
        }

        return result;
    }
}