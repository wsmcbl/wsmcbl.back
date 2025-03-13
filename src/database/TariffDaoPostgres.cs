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
            .AsNoTracking()
            .Where(e => e.schoolyearId == schoolyear.id)
            .Where(e => e.type == Const.TARIFF_MONTHLY)
            .Where(e => e.isLate == false)
            .ToListAsync();

        tariffs.ForEach(t => t.checkDueDate());

        return tariffs.Where(t => t.isLate).OrderBy(t => t.tariffId).ToList();
    }

    public async Task<List<TariffEntity>> getListByStudent(string studentId)
    {
        var currentSch = await daoFactory.schoolyearDao!.getCurrentOrNew();
        var newSch = await daoFactory.schoolyearDao!.getNewOrCurrent();

        var debtList = await context.Set<DebtHistoryEntity>()
            .Where(e => e.studentId == studentId)
            .Where(e => e.schoolyear == currentSch.id || e.schoolyear == newSch.id || !e.isPaid)
            .Include(e => e.tariff)
            .ToListAsync();

        return debtList.Select(e => e.tariff).ToList();
    }

    public async Task<TariffEntity> getRegistrationTariff(string schoolyearId, int level)
    {
        var tariff = await entities
            .FirstOrDefaultAsync(e => e.schoolyearId == schoolyearId
                                      && e.educationalLevel == level && e.type == Const.TARIFF_REGISTRATION);
        
        if (tariff == null)
        {
            throw new EntityNotFoundException(
                $"Tariff with educationalLevel ({level}) in current schoolyear not found.");
        }

        return tariff;
    }

    public async Task<List<TariffEntity>> getCurrentRegistrationTariffList()
    {
        var schoolyear = await daoFactory.schoolyearDao!.getNewOrCurrent();
        return await entities
            .Where(e => e.schoolyearId == schoolyear.id && e.type == Const.TARIFF_REGISTRATION)
            .ToListAsync();
    }

    public async Task createRange(List<TariffEntity> tariffList)
    {
        entities.AddRange(tariffList);
        await saveAsync();
    }
}