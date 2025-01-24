using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.database;

public class ExchangeRateDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<ExchangeRateEntity, int>(context), IExchangeRateDao
{
    public async Task<ExchangeRateEntity> getLastRate()
    {
        var daoFactory = new DaoFactoryPostgres(context);
        var schoolyear = await daoFactory.schoolyearDao!.getNewOrCurrent();
        
        return await entities.Where(e => e.schoolyear == schoolyear.id).FirstAsync();
    }
}