using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.database;

public class DebtHistoryDaoPostgres(PostgresContext context) : GenericDaoPostgres<DebtHistoryEntity, string>(context), IDebtHistoryDao
{
    public async Task<List<DebtHistoryEntity>> getListByStudent(string studentId)
    {
        return await entities
            .Where(e => e.studentId == studentId)
            .Include(e => e.tariff)
            .ToListAsync();
    }

    public async Task<List<DebtHistoryEntity>> getListByStudentWithPayments(string studentId)
    {
        var history = await getListByStudent(studentId);
        return history.Where(dh => dh.isPaid || dh.havePayments()).ToList();
    }

    public async Task exonerateArrears(string studentId, List<DebtHistoryEntity> list)
    {
        if (list.Count == 0)
        {
            return;
        }
        
        var debts = await entities.Where(e => e.studentId == studentId).ToListAsync();
        
        foreach (var item in list)
        {
            var debt = debts.Find(e => e.tariffId == item.tariffId);

            if (debt == null)
            {
                continue;
            }

            debt.arrears = 0;
            update(debt);
        }
    }

    public async Task<bool> haveTariffsAlreadyPaid(TransactionEntity transaction)
    {
        var debts = await entities
            .Where(e => e.studentId == transaction.studentId)
            .Where(e => e.isPaid)
            .ToListAsync();

        var detail = transaction.details
            .FirstOrDefault(t => debts.Exists(e => e.tariffId == t.tariffId));
        
        return detail != null;
    }

    public async Task<List<DebtHistoryEntity>> getListByTransaction(TransactionEntity transaction)
    {
        var tariffIdList = transaction.getTariffIdList();
        return await entities
            .Where(e => e.studentId == transaction.studentId)
            .Where(e => tariffIdList.Contains(e.tariffId))
            .Include(e => e.tariff)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task restoreDebt(string transactionId)
    {
        var transactionDao = new TransactionDaoPostgres(context);
        var transaction = await transactionDao.getById(transactionId);

        if (!transaction!.isValid)
        {
            throw new ConflictException("The transaction is already cancelled.");
        }

        var debtList = await entities.Where(e => e.studentId == transaction.studentId).ToListAsync();

        foreach (var item in transaction.details)
        {
            var debt = debtList.FirstOrDefault(e => e.tariffId == item.tariffId);
            
            if (debt == null)
            {
                continue;
            }

            debt.restoreDebt(item.amount);
            update(debt);
        }
    }

    public async Task<DebtHistoryEntity> forgiveADebt(string studentId, int tariffId)
    {
        var debt = await entities
            .Where(e => e.studentId == studentId && e.tariffId == tariffId)
            .FirstOrDefaultAsync();
        
        if (debt == null)
        {
            throw new EntityNotFoundException($"Entity of type (debt) with student ({studentId}) and tariff ({tariffId}) not found.");
        }

        debt.forgiveDebt();
        update(debt);
        
        return debt;
    }

    public async Task addRegistrationTariffDebtByStudent(StudentEntity student)
    {
        var debt = new DebtHistoryEntity
        {
            studentId = student.studentId!
        };
        
        create(debt);
        await context.SaveChangesAsync();
    }
}