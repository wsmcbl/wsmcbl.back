using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.database;

public class TariffDaoPostgres(PostgresContext context) : GenericDaoPostgres<TariffEntity, int>(context), ITariffDao
{
    private string? currentSchoolyearId { get; set; }
    private string? schoolyearLabel { get; set; }

    private async Task setSchoolyearIds()
    {
        var schoolyearDao = new SchoolyearDaoPostgres(context);
        var ID = await schoolyearDao.getCurrentAndNewSchoolyearIds();
        currentSchoolyearId = ID.currentSchoolyear;

        if (currentSchoolyearId != "")
        {
            var currentSchoolyear = await schoolyearDao.getCurrentSchoolyear();
            schoolyearLabel = currentSchoolyear.label;
        }
    }
    
    public async Task<List<TariffEntity>> getOverdueList()
    {
        await setSchoolyearIds();
        
        var tariffs = await entities
            .Where(t => t.schoolYear == currentSchoolyearId && t.isLate && t.type == Const.TARIFF_MONTHLY)
            .ToListAsync();

        tariffs.ForEach(t => t.checkDueDate());

        return tariffs.Where(t => t.isLate).OrderBy(t => t.tariffId).ToList();
    }

    public async Task<List<TariffEntity>> getListByStudent(string studentId)
    {
        await setSchoolyearIds();
        
        var debts = context.Set<DebtHistoryEntity>().Where(d => d.studentId == studentId);
        
        debts.Where(d => d.schoolyear == currentSchoolyearId || !d.isPaid)
            .Include(d => d.tariff);
        
        var tariffList = await debts.Select(d => d.tariff).ToListAsync();

        foreach (var item in tariffList)
        {
            if (item.schoolYear == currentSchoolyearId)
            {
                item.schoolYear = schoolyearLabel;
            }
            else
            {
                var dao = new SchoolyearDaoPostgres(context);
                var schoolyear = await dao.getById(item.schoolYear!);
                if (schoolyear != null)
                {
                    item.schoolYear = schoolyear.label;
                }
            }
        }

        return tariffList;
    }

    public async Task<float[]> getGeneralBalance(string studentId)
    {
        await setSchoolyearIds();
        
        var debts = await context.Set<DebtHistoryEntity>()
            .Where(d => d.studentId == studentId && d.schoolyear == currentSchoolyearId)
            .Include(d => d.tariff)
            .Where(d => d.tariff.type == Const.TARIFF_MONTHLY)
            .ToListAsync();
        
        float[] balance = [0, 0];
        
        foreach (var debt in debts)
        {
            balance[1] += debt.tariff.amount;
                
            if (debt.isPaid)
            {
                balance[0] += debt.tariff.amount;
            }
        }

        return balance;
    }

    public void createList(List<TariffEntity> tariffs)
    {
        foreach (var item in tariffs)
        {
            create(item);
        }
    }
}