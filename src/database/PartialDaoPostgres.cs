using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.database;

public class PartialDaoPostgres(PostgresContext context) : GenericDaoPostgres<PartialEntity, int>(context), IPartialDao
{
    public async Task<List<PartialEntity>> getListByCurrentSchoolyear()
    {
        var schoolyear = DateTime.Today.Year.ToString();

        FormattableString query =
            $@"select p.* from academy.partial p
               inner join academy.semester s on p.semesterid = s.semesterid
               inner join secretary.schoolyear sy on sy.schoolyearid = s.schoolyear
               where sy.label = {schoolyear}";

        return await context.Set<PartialEntity>()
            .FromSqlInterpolated(query)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<PartialEntity>> getListByStudentId(string studentId)
    {
        var partials = await getListByCurrentSchoolyear();

        foreach (var partial in partials)
        {//////////////////////////////////////////////////
            partial.grades = await context.Set<GradeEntity>()
                .Where(e => e.studentId == studentId && partial.partialId == 100000000000)
                .AsNoTracking()
                .ToListAsync();
        }

        return partials;
    }
}