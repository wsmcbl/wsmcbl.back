using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.database;

public class DegreeDataDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<DegreeDataEntity, string>(context), IDegreeDataDao
{
    public new async Task<List<DegreeDataEntity>> getAll()
    {
        return await entities.Include(e => e.subjectList).ToListAsync();
    }
}