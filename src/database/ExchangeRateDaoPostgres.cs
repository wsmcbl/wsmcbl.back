using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.database;

public class ExchangeRateDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<ExchangeRateEntity, int>(context), IExchangeRateDao
{
    public async Task<ExchangeRateEntity> getLastRate()
    {
        var daoFactory = new DaoFactoryPostgres(context);
        var schoolyear = await daoFactory.schoolyearDao.getNewOrCurrent();

        var result = await entities.FirstOrDefaultAsync(e => e.schoolyearId == schoolyear.id);
        if (result == null)
        {
            throw new EntityNotFoundException("Exchange rate not found");
        }
        
        return result;
    }
}