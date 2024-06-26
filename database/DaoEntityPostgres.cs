using Microsoft.EntityFrameworkCore;
using wsmcbl.back.exception;
using wsmcbl.back.model.accounting;
using wsmcbl.back.model.config;

namespace wsmcbl.back.database;

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
        var tariffs = await context.Tariff
            .Where(t => t.schoolYear == schoolyear && t.isLate && t.type == 1)
            .ToListAsync();

        tariffs.ForEach(t => t.checkDueDate());

        return tariffs.Where(t => t.isLate).OrderBy(t => t.tariffId).ToList();
    }

    public async Task<List<TariffEntity>> getListByStudent(string studentId)
    {
        var debts = await Task
            .FromResult(context.DebtHistory
                .Where(d => d.studentId == studentId)
                .Include(d => d.tariff));
        
        var list = new List<TariffEntity>();
        
        foreach (var debt in debts)
        {
            if(debt.schoolyear == schoolyear)
            {
                list.Add(debt.tariff);
            }
            else if (!debt.isPaid)
            {
                 list.Add(debt.tariff);
            }
        }
        
        return list;
    }

    public async Task<float[]> getGeneralBalance(string studentId)
    {
        var debts = await Task
            .FromResult(context.DebtHistory
                .Where(d => d.studentId == studentId && d.schoolyear == schoolyear)
                .Include(d => d.tariff));
        
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
        return await context.Student_accounting.Include(d => d.student).ToListAsync();
    }

    public new async Task<StudentEntity?> getById(string id)
    {
        var student = await context.Student_accounting
            .Include(d => d.student)
            .Include(d => d.discount)
            .Include(e => e.transactions)!
                .ThenInclude(t => t.details)
                .FirstOrDefaultAsync(e => e.studentId == id);

        if (student is null)
        {
            throw new EntityNotFoundException("Student", id);
        }
        
        var service = new TransactionDaoPostgres(context);
        foreach (var transaction in student.transactions!)
        {
            foreach (var item in transaction.details)
            {
                await service.setTariff(item);
            }
        }
        
        return student;
    }
}

public class TransactionDaoPostgres(PostgresContext context) 
    : GenericDaoPostgres<TransactionEntity, string>(context), ITransactionDao
{
    
    public override async Task create(TransactionEntity entity)
    {
        entity.computeTotal();
        
        try
        {
            await base.create(entity);
        }
        catch (DbUpdateException)
        {
            throw new DbException("Transaction");
        }
    }

    internal async Task setTariff(TransactionTariffEntity detail)
    {
        var service = new TariffDaoPostgres(context);
        var tariff = await service.getById(detail.tariffId);

        if (tariff is null)
        {
            throw new EntityNotFoundException("Tariff", detail.tariffId.ToString());
        }
        
        detail.setTariff(tariff);
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
        var history = await context.DebtHistory
            .Where(dh => dh.studentId == studentId)
            .Include(dh => dh.tariff)
            .ToListAsync();

        return history.Where(dh => dh.havePayments()).ToList();
    }

    public async Task exonerateArrears(List<DebtHistoryEntity> list)
    {
        foreach (var item in list)
        {
            var debt = await context.DebtHistory
                .Where(dh => dh.studentId == item.studentId && dh.tariffId == item.tariffId)
                .FirstOrDefaultAsync();

            if (debt == null)
            {
                continue;
            }
            
            debt.arrear = 0;
            context.Update(debt);
        }
        await context.SaveChangesAsync();
    }
}