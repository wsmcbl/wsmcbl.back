using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.database;

public class DebtHistoryDaoPostgres : GenericDaoPostgres<DebtHistoryEntity, string>, IDebtHistoryDao
{
    private DaoFactory daoFactory { get; set; }
    
    public DebtHistoryDaoPostgres(PostgresContext context) : base(context)
    {
        daoFactory = new DaoFactoryPostgres(context);
    }
    
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
        var transaction = await daoFactory.transactionDao!.getById(transactionId);
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
        var tariff = await daoFactory.tariffDao!.getAllInCurrentSchoolyear(student.educationalLevel);

        var debt = new DebtHistoryEntity(student.studentId!, tariff);
        
        var schoolyear = await daoFactory.schoolyearDao!.getCurrent();
        debt.schoolyear = schoolyear.id!;
        
        create(debt);
        await saveAsync();
    }

    public async Task deleteRange(List<DebtHistoryEntity> debtList)
    {
        entities.RemoveRange(debtList);
        await saveAsync();
    }
    
    public async Task<float[]> getGeneralBalance(string studentId)
    {
        var currentSch = await daoFactory.schoolyearDao!.getCurrentOrNew();
        var newSch = await daoFactory.schoolyearDao!.getNewOrCurrent();
        
        var debts = await entities.Where(d => d.studentId == studentId)
            .Where(d => d.schoolyear == currentSch.id || d.schoolyear == newSch.id)
            .Include(d => d.tariff)
            .Where(d => d.tariff.type == Const.TARIFF_MONTHLY)
            .ToListAsync();

        float[] balance = [0, 0];

        foreach (var debt in debts)
        {
            balance[0] += debt.debtBalance;
            balance[1] += debt.amount;
        }

        return balance;
    }
}