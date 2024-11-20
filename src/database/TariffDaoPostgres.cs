using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.model;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.database;

public class TariffDaoPostgres(PostgresContext context) : GenericDaoPostgres<TariffEntity, int>(context), ITariffDao
{
    private string? currentSchoolyearId { get; set; }
    private string? newSchoolyearId { get; set; }
    private string? schoolyearLabel { get; set; }

    private async Task setSchoolyearIds()
    {
        var schoolyearDao = new SchoolyearDaoPostgres(context);
        var ID = await schoolyearDao.getCurrentAndNewSchoolyearIds();
        currentSchoolyearId = ID.currentSchoolyear;
        newSchoolyearId = ID.newSchoolyear;

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
            .Where(e => e.schoolYear == currentSchoolyearId)
            .Where(e => e.isLate && e.type == Const.TARIFF_MONTHLY)
            .ToListAsync();

        tariffs.ForEach(t => t.checkDueDate());

        return tariffs.Where(t => t.isLate).OrderBy(t => t.tariffId).ToList();
    }

    public async Task<List<TariffEntity>> getListByStudent(string studentId)
    {
        await setSchoolyearIds();

        var debts = await context.Set<DebtHistoryEntity>()
            .Where(e => e.studentId == studentId)
            .Where(e => e.schoolyear == currentSchoolyearId || e.schoolyear == newSchoolyearId || !e.isPaid)
            .Include(e => e.tariff)
            .ToListAsync();

        
        return debts.Select(e => updateTariffSchoolyear(e.tariff)).ToList();
    }

    private TariffEntity updateTariffSchoolyear(TariffEntity entity)
    {
        if (entity.schoolYear == currentSchoolyearId)
        {
            entity.schoolYear = schoolyearLabel;
            return entity;
        }

        var dao = new SchoolyearDaoPostgres(context);
        var schoolyear = dao.getById(entity.schoolYear!).GetAwaiter().GetResult();
        if (schoolyear != null)
        {
            entity.schoolYear = schoolyear.label;
        }

        return entity;
    }

    public async Task<float[]> getGeneralBalance(string studentId)
    {
        await setSchoolyearIds();

        var debts = await context.Set<DebtHistoryEntity>()
            .Where(d => d.studentId == studentId)
            .Where(d => d.schoolyear == currentSchoolyearId || d.schoolyear == newSchoolyearId)
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

    public void createList(List<TariffEntity> tariffs)
    {
        foreach (var item in tariffs)
        {
            create(item);
        }
    }
}