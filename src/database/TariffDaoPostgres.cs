using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.database;

public class TariffDaoPostgres(PostgresContext context) : GenericDaoPostgres<TariffEntity, int>(context), ITariffDao
{
    private string? schoolyear;
    
    private async Task initSchoolyear()
    {
        if (schoolyear != null)
        {
            return;
        }
        
        var year = DateTime.Today.Year.ToString();
        var element = await context.Set<SchoolYearEntity>().FirstOrDefaultAsync(e => e.label == year);

        if (element == null)
        {
            throw new ArgumentException("Schoolyear no exist");
        }
        
        schoolyear = element.id;
    }
    
    public async Task<List<TariffEntity>> getOverdueList()
    {
        await initSchoolyear();
        
        var tariffs = await entities.Where(t => t.schoolYear == schoolyear && t.isLate && t.type == 1)
            .ToListAsync();

        tariffs.ForEach(t => t.checkDueDate());

        return tariffs.Where(t => t.isLate).OrderBy(t => t.tariffId).ToList();
    }

    public async Task<List<TariffEntity>> getListByStudent(string studentId)
    {
        await initSchoolyear();
        
        var debts = context.Set<DebtHistoryEntity>().Where(d => d.studentId == studentId);
        
        debts.Where(d => d.schoolyear == schoolyear || !d.isPaid)
            .Include(d => d.tariff);
        
        var list = debts.Select(d => d.tariff);

        return await list.ToListAsync();
    }

    public async Task<float[]> getGeneralBalance(string studentId)
    {
        await initSchoolyear();
        
        var debts = await context.Set<DebtHistoryEntity>()
            .Where(d => d.studentId == studentId && d.schoolyear == schoolyear)
            .Include(d => d.tariff)
            .Where(d => d.tariff.type == 1)
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