using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.database;

public class TariffDaoPostgres : GenericDaoPostgres<TariffEntity, int>, ITariffDao
{
    private DaoFactory daoFactory { get; set; }

    public TariffDaoPostgres(PostgresContext context) : base(context)
    {
        daoFactory = new DaoFactoryPostgres(context);
    }

    public async Task<List<TariffEntity>> getOverdueList()
    {
        var schoolyear = await daoFactory.schoolyearDao!.getCurrentOrNew();

        var tariffs = await entities
            .Where(e => e.schoolYear == schoolyear.id)
            .Where(e => e.isLate && e.type == Const.TARIFF_MONTHLY)
            .ToListAsync();

        tariffs.ForEach(t => t.checkDueDate());

        return tariffs.Where(t => t.isLate).OrderBy(t => t.tariffId).ToList();
    }

    public async Task<List<TariffEntity>> getListByStudent(string studentId)
    {
        var currentSch = await daoFactory.schoolyearDao!.getCurrentOrNew();
        var newSch = await daoFactory.schoolyearDao!.getNewOrCurrent();
        
        var debts = await context.Set<DebtHistoryEntity>()
            .Where(e => e.studentId == studentId)
            .Where(e => e.schoolyear == currentSch.id || e.schoolyear == newSch.id || !e.isPaid)
            .Include(e => e.tariff)
            .ToListAsync();
        
        return debts.Select(e => e.tariff).ToList();
    }
    
    public async Task<TariffEntity> getAllInCurrentSchoolyear(int level)
    {
        var schoolyear = await daoFactory.schoolyearDao!.getCurrent(false);
        
        var tariff = await entities
            .FirstOrDefaultAsync(e => e.educationalLevel == level && e.schoolYear == schoolyear.id);
        if (tariff == null)
        {
            throw new EntityNotFoundException($"Tariff with educationalLevel ({level}) in current schoolyear not found.");
        }

        return tariff;
    }

    public async Task createRange(List<TariffEntity> tariffList)
    {
        entities.AddRange(tariffList);
        await saveAsync();
    }
}