using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.database.service;
using wsmcbl.src.exception;
using wsmcbl.src.model;
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

    public async Task<PagedResult<TransactionReportView>> getAll(TransactionReportViewPagedRequest request)
    {
        var query = context.GetQueryable<TransactionReportView>();

        if (request is { from: not null, to: not null })
        {
            query = query.Where(e => e.dateTime >= request.From() && e.dateTime <= request.To());
        }

        var pagedService = new PagedService<TransactionReportView>(query, search);
        
        request.setDefaultSort("number");
        return await pagedService.getPaged(request);
    }
    
    private IQueryable<TransactionReportView> search(IQueryable<TransactionReportView> query, string search)
    { 
        var value = $"%{search}%";
        
        return query.Where(e =>
            EF.Functions.Like(e.transactionId, value) ||
            EF.Functions.Like(e.studentId, value) ||
            EF.Functions.Like(e.studentName.ToLower(), value) ||
            (e.enrollmentLabel != null && EF.Functions.Like(e.enrollmentLabel.ToLower(), value)));
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