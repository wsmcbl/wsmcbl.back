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
    
    public async Task<List<DebtHistoryEntity>> getListByStudentId(string studentId, bool withTariff = true)
    {
        var query = entities.Where(e => e.studentId == studentId);
        if (withTariff)
        {
            query = query.Include(e => e.tariff);
        }
        
        return await query.ToListAsync();
    }

    public async Task<List<DebtHistoryEntity>> getListByStudentWithPayments(string studentId)
    {
        var debtList = await getListByStudentId(studentId);
        return debtList.Where(dh => dh.isPaid || dh.havePayments()).ToList();
    }

    public async Task exonerateArrears(string studentId, List<DebtHistoryEntity> list)
    {
        if (list.Count == 0)
        {
            return;
        }
        
        var debtList = await getListByStudentId(studentId,false);
        
        foreach (var item in list)
        {
            var debt = debtList.FirstOrDefault(e => e.tariffId == item.tariffId);
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
        var debtList = await entities
            .Where(e => e.studentId == transaction.studentId)
            .Where(e => e.isPaid)
            .ToListAsync();

        var detail = transaction.details.FirstOrDefault(t
            => debtList.Exists(e => e.tariffId == t.tariffId));
        
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

        var debtList = await getListByStudentId(transaction.studentId, false);

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
        var debt = await entities.Where(e => e.studentId == studentId && e.tariffId == tariffId)
            .FirstOrDefaultAsync();
        
        if (debt == null)
        {
            throw new EntityNotFoundException($"Entity of type (debt) with student ({studentId}) and tariff ({tariffId}) not found.");
        }

        debt.forgiveDebt();
        update(debt);
        
        return debt;
    }

    public async Task createRegistrationDebtByStudent(StudentEntity student)
    {
        var schoolyear = await daoFactory.schoolyearDao!.getCurrent();
        var tariff = await daoFactory.tariffDao!.getRegistrationTariff(schoolyear.id!, student.educationalLevel);
 
        var debt = new DebtHistoryEntity(student.studentId!, tariff)
        {
            schoolyear = schoolyear.id!
        };

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
        
        var debtList = await entities.Where(d => d.studentId == studentId)
            .Where(d => d.schoolyear == currentSch.id || d.schoolyear == newSch.id)
            .Include(d => d.tariff)
            .Where(d => d.tariff.type == Const.TARIFF_MONTHLY)
            .ToListAsync();

        float[] balance = [0, 0];

        foreach (var debt in debtList)
        {
            balance[0] += debt.debtBalance;
            balance[1] += debt.amount;
        }

        return balance;
    }
}