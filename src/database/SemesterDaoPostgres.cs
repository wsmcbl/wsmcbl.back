using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.database;

public class SemesterDaoPostgres(PostgresContext context) : GenericDaoPostgres<SemesterEntity, int>(context), ISemesterDao
{
    public async Task<List<SemesterEntity>> getListInCurrentSchoolyear()
    {
        DaoFactory daoFactory = new DaoFactoryPostgres(context);
        var schoolyear = await daoFactory.schoolyearDao!.getCurrent();
        
        return await entities.Where(e => e.schoolyearId == schoolyear.id).AsNoTracking().ToListAsync();
    }
}