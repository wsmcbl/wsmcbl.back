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
    public new async Task<List<TariffEntity>> getAll()
    {
        var elements = await base.getAll();
        return elements.OrderBy(e => e.tariffId).ToList();
    }

    public async Task<List<TariffEntity>> getAll(string schoolyear)
    {
        var tariffs = await Task
            .FromResult(context.Tariff
                .Where(t => t.schoolYear == schoolyear && t.isLate == false && t.dueDate != null)
                .ToList());

        foreach (var item in tariffs)
        {
            item.checkDueDate();
        }

        return tariffs.Where(t => t.isLate == true).ToList();
    }
}

public class StudentDaoPostgres(PostgresContext context) 
    : GenericDaoPostgres<StudentEntity, string>(context), IStudentDao
{
    public new async Task<List<StudentEntity>> getAll()
    {
        return await Task.FromResult(context.Student_accounting.Include(d => d.student).ToList());
    }

    public new async Task<StudentEntity?> getById(string id)
    {
        var student = context.Student_accounting
                .Include(d => d.student)
                .Include(d => d.discount)
                .Include(e => e.transactions)
                .ThenInclude(t => t.details)
                .FirstOrDefault(e => e.studentId == id);

        if (student is null)
        {
            throw new EntityNotFoundException("Student", id);
        }
        
        var service = new TransactionDaoPostgres(context);
        foreach (var transaction in student.transactions)
        {
            foreach (var item in transaction.details)
            {
                await service.setTariff(item);
            }
        }
        
        return await Task.FromResult(student);
    }
}

public class TransactionDaoPostgres(PostgresContext context) 
    : GenericDaoPostgres<TransactionEntity, string>(context), ITransactionDao
{
    public override async Task create(TransactionEntity entity)
    {
        foreach (var item in entity.details)
        {
            await setTariff(item);
            item.computeSubTotal();
            item.applyArrears();
        }
        
        entity.computeTotal();
        
        await base.create(entity);
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
    
    public async Task<TransactionEntity?> getLastByStudentId(string studentId)
    {
        var service = new StudentDaoPostgres(context);

        var student = await service.getById(studentId);

        if (student is null)
        {
            throw new EntityNotFoundException("Student", studentId);
        }

        return student?.getLastTransaction();
    }
}
    
public class SecretaryStudentDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<model.secretary.StudentEntity, string>(context), model.secretary.IStudentDao;
