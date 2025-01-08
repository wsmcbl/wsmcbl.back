using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.database;

public class ExchangeRateDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<ExchangeRateEntity, int>(context), IExchangeRateDao
{
    public async Task<ExchangeRateEntity> getLastRate()
    {
        var schoolyearId = await getLastSchoolyearId();
        return await entities.Where(e => e.schoolyear == schoolyearId).FirstAsync();
    }

    private async Task<string> getLastSchoolyearId()
    {
        var schoolyearDao = new SchoolyearDaoPostgres(context);
        var ID = await schoolyearDao.getCurrentAndNewSchoolyearIds();

        return ID.newSchoolyear != "" ? ID.newSchoolyear : ID.currentSchoolyear;
    }
}