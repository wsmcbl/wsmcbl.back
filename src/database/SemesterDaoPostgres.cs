using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.database;

public class SemesterDaoPostgres(PostgresContext context) : GenericDaoPostgres<SemesterEntity, int>(context), ISemesterDao
{
    public async Task<List<SemesterEntity>> getAllOfCurrentSchoolyear()
    {
        var schoolyear = DateTime.Today.Year.ToString();

        FormattableString query = $@"select s.* from academy.semester s
               inner join secretary.schoolyear sy on sy.schoolyearid = s.schoolyear
               where sy.label = {schoolyear}";

        var result = await entities.FromSqlInterpolated(query)
            .AsNoTracking()
            .ToListAsync();

        return result;
    }
}