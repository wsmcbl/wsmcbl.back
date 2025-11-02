using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.database.service;
using wsmcbl.src.dto.accounting;
using wsmcbl.src.exception;
using wsmcbl.src.model;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;
using StudentEntity = wsmcbl.src.model.accounting.StudentEntity;

namespace wsmcbl.src.database;

public class DebtHistoryDaoPostgres : GenericDaoPostgres<DebtHistoryEntity, string>, IDebtHistoryDao
{
    private DaoFactory daoFactory { get; set; }
    
    public DebtHistoryDaoPostgres(PostgresContext context) : base(context)
    {
        daoFactory = new DaoFactoryPostgres(context);
    }

    public async Task<PagedResult<DebtHistoryEntity>> getPaginatedByStudentId(string studentId, PagedRequest request)
    {
        var query = context.GetQueryable<DebtHistoryEntity>()
            .Where(e => e.studentId == studentId)
            .Include(e => e.tariff);
        
        var pagedService = new PagedService<DebtHistoryEntity>(query, searchInDebtHistory);
        
        request.setDefaultSort("tariffId");   
        return await pagedService.getPaged(request);
    }
    
    private static IQueryable<DebtHistoryEntity> searchInDebtHistory(IQueryable<DebtHistoryEntity> query, string search)
    { 
        var value = $"%{search}%";
        
        return query.Where(e =>
            EF.Functions.Like(e.tariffId.ToString(), value) ||
            EF.Functions.Like(e.schoolyear.ToLower(), value));
    }

    public async Task<GenerateDebtsResult> generateStudentDebts(string studentId, int educationalLevel, string schoolyearId)  
    {  
        var result = await context.Database.SqlQueryRaw<GenerateDebtsResult>(  
            "SELECT * FROM Accounting.GenerateStudentDebts(@p0, @p1, @p2)",  
            studentId, educationalLevel, schoolyearId  
        ).ToListAsync();  
      
        return result.FirstOrDefault() ?? new GenerateDebtsResult();  
    }
    
    public async Task<List<DebtHistoryEntity>> getListByStudentId(string studentId)
    {
        var debtList = await getList(studentId);
        return debtList.Where(dh => dh.isPaid || dh.havePayments()).ToList();
    }

    public async Task exonerateArrears(string studentId, List<DebtHistoryEntity> list)
    {
        if (list.Count == 0)
        {
            return;
        }
        
        var debtList = await getList(studentId,false);
        
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

        await saveAsync();
    }

    public async Task<bool> hasPaidTariffsInTransaction(TransactionEntity transaction)
    {
        var debtList = await entities
            .Where(e => e.studentId == transaction.studentId)
            .Where(e => e.isPaid)
            .ToListAsync();

        var detail = transaction.details.FirstOrDefault(t
            => debtList.Exists(e => e.tariffId == t.tariffId));
        
        return detail != null;
    }

    public async Task<List<DebtHistoryEntity>> getListByTransactionId(TransactionEntity transaction)
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
            throw new UpdateConflictException("Transaction", "The transaction is already cancelled.");
        }

        var debtList = await getList(transaction.studentId, false);

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
    
    public async Task<decimal[]> getGeneralBalance(string studentId)
    {
        var currentSch = await daoFactory.schoolyearDao!.getCurrentOrNew();
        var newSch = await daoFactory.schoolyearDao!.getNewOrCurrent();
        
        var debtList = await entities.Where(d => d.studentId == studentId)
            .Where(d => d.schoolyear == currentSch.id || d.schoolyear == newSch.id)
            .Include(d => d.tariff)
            .Where(d => d.tariff.type == Const.TARIFF_MONTHLY)
            .ToListAsync();

        decimal[] balance = [0, 0];

        foreach (var debt in debtList)
        {
            balance[0] += debt.debtBalance;
            balance[1] += debt.amount;
        }

        return balance;
    }
    
    private async Task<List<DebtHistoryEntity>> getList(string studentId, bool withTariff = true)
    {
        var query = entities.Where(e => e.studentId == studentId);
        if (withTariff)
        {
            query = query.Include(e => e.tariff);
        }
        
        return await query.ToListAsync();
    }

    public async Task<List<DebtHistoryEntity>> getAllByMonth(DateTime startDate, bool paid = false)
    {
        SchoolyearEntity schoolyear;
        
        try
        {
            schoolyear = await daoFactory.schoolyearDao!.getByLabel(startDate.Year);
        }
        catch (Exception)
        {
            return [];
        }
     
        var from = new DateOnly(startDate.Year, startDate.Month, 1);
        var to = from.AddMonths(1);

        var set = paid ? entities.Where(e => e.isPaid) : entities;
        
        return await set.AsNoTracking().Join
        (
            context.Set<TariffEntity>().Where(e => e.schoolyearId == schoolyear.id && e.dueDate >= from && e.dueDate < to),
            debt => debt.tariffId,
            tariff => tariff.tariffId,
            (debt, tariff) => new DebtHistoryEntity
            {
                studentId = debt.studentId,
                tariffId = debt.tariffId,
                schoolyear = debt.schoolyear,
                arrears = debt.arrears,
                subAmount = debt.subAmount,
                amount = debt.amount,
                debtBalance = debt.debtBalance,
                isPaid = debt.isPaid,
                tariff = tariff
            }
        ).ToListAsync();
    }
}