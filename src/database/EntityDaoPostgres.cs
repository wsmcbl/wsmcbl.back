using Microsoft.EntityFrameworkCore;
using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.config;

namespace wsmcbl.src.database;

public class UserDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<UserEntity, string>(context), IUserDao;

public class CashierDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<CashierEntity, string>(context), ICashierDao
{
    public override async Task<CashierEntity?> getById(string id)
    {
        var cashier = await base.getById(id);

        if (cashier is null)
        {
            throw new EntityNotFoundException("Cashier", id);
        }
        
        var service = new UserDaoPostgres(context);
        var user = await service.getById(cashier.userId);

        cashier.user = user!;
        
        return cashier;
    }
}

public class TariffDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<TariffEntity, int>(context), ITariffDao
{
    private readonly string schoolyear = DateTime.Today.Year.ToString(); 

    public async Task<List<TariffEntity>> getOverdueList()
    {
        var tariffs = await entities.Where(t => t.schoolYear == schoolyear && t.isLate && t.type == 1)
            .ToListAsync();

        tariffs.ForEach(t => t.checkDueDate());

        return tariffs.Where(t => t.isLate).OrderBy(t => t.tariffId).ToList();
    }

    public async Task<List<TariffEntity>> getListByStudent(string studentId)
    {
        var debts = context.DebtHistory.Where(d => d.studentId == studentId);
        
        debts.Where(d => d.schoolyear == schoolyear || !d.isPaid)
            .Include(d => d.tariff);
        
        var list = debts.Select(d => d.tariff);

        return await list.ToListAsync();
    }

    public async Task<float[]> getGeneralBalance(string studentId)
    {
        var debts = await context.DebtHistory
                .Where(d => d.studentId == studentId && d.schoolyear == schoolyear)
                .Include(d => d.tariff).ToListAsync();
        
        float[] balance = [0, 0];
        
        foreach (var debt in debts)
        {
            if (debt.tariff.type != 1)
            {
                continue;
            }
            
            balance[1] += debt.tariff.amount;
                
            if (debt.isPaid)
            {
                balance[0] += debt.tariff.amount;
            }
        }

        return balance;
    }
}

public class StudentDaoPostgres(PostgresContext context) 
    : GenericDaoPostgres<StudentEntity, string>(context), IStudentDao
{
    public new async Task<List<StudentEntity>> getAll()
    {
        return await entities.Include(d => d.student).ToListAsync();
    }

    public new async Task<StudentEntity?> getById(string id)
    {
        var student = await entities
            .Include(e => e.discount)
            .Include(e => e.student)
            .Include(e => e.transactions)!
            .ThenInclude(t => t.details)
            .FirstOrDefaultAsync(e => e.studentId == id);
        
        if (student is null)
        {
            throw new EntityNotFoundException("Student", id);
        }
        
        foreach (var transaction in student.transactions!)
        {
            foreach (var item in transaction.details)
            {
                var tariff = await context.Tariff.FirstOrDefaultAsync(t => t.tariffId == item.tariffId);
                item.setTariff(tariff);
            }
        }
        
        return student;
    }
}

public class TransactionDaoPostgres(PostgresContext context) 
    : GenericDaoPostgres<TransactionEntity, string>(context), ITransactionDao
{
    public override void create(TransactionEntity entity)
    {
        entity.computeTotal();
        base.create(entity);
    }
}
    
public class SecretaryStudentDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<model.secretary.StudentEntity, string>(context), model.secretary.IStudentDao;

public class TariffTypeDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<TariffTypeEntity, int>(context), ITariffTypeDao;

public class DebtHistoryDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<DebtHistoryEntity, string>(context), IDebtHistoryDao
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
}