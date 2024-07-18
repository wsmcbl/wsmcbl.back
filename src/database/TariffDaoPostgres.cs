using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.database;

public class TariffDaoPostgres(PostgresContext context) : GenericDaoPostgres<TariffEntity, int>(context), ITariffDao
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
        var debts = context.Set<DebtHistoryEntity>().Where(d => d.studentId == studentId);
        
        debts.Where(d => d.schoolyear == schoolyear || !d.isPaid)
            .Include(d => d.tariff);
        
        var list = debts.Select(d => d.tariff);

        return await list.ToListAsync();
    }

    public async Task<float[]> getGeneralBalance(string studentId)
    {
        var debts = await context.Set<DebtHistoryEntity>()
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

    public void createList(List<TariffEntity> tariffs)
    {
        throw new NotImplementedException();
    }
}