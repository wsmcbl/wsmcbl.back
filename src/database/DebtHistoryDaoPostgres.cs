using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.database;

public class DebtHistoryDaoPostgres(PostgresContext context) : GenericDaoPostgres<DebtHistoryEntity, string>(context), IDebtHistoryDao
{
    public async Task<List<DebtHistoryEntity>> getListByStudent(string studentId)
    {
        var history = await entities
            .Where(dh => dh.studentId == studentId)
            .Include(dh => dh.tariff)
            .ToListAsync();

        return history.Where(dh => dh.havePayments()).ToList();
    }

    public async Task exonerateArrears(string studentId, List<DebtHistoryEntity> list)
    {
        if (list.Count == 0)
        {
            return;
        }
        
        var debts = await entities.Where(dh => dh.studentId == studentId).ToListAsync();
        
        foreach (var item in list)
        {
            var debt = debts.Find(dh => dh.tariffId == item.tariffId);

            if (debt == null)
            {
                continue;
            }

            debt.arrear = 0;
            update(debt);
        }
    }

    public async Task checkIsPaid(TransactionEntity transaction)
    {
        var debts = await entities
            .Where(e => e.studentId == transaction.studentId)
            .Where(e => e.isPaid)
            .ToListAsync();

        var detail = transaction.details
            .FirstOrDefault(t => debts.Exists(e => e.tariffId == t.tariffId));
        
        if(detail != null)
        {
            throw new ArgumentException($"Tariff with ID: {detail.tariffId} is already paid");
        }
    }
}