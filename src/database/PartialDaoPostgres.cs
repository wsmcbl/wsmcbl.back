using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.database;

public class PartialDaoPostgres(PostgresContext context) : GenericDaoPostgres<PartialEntity, int>(context), IPartialDao
{
    public async Task<List<PartialEntity>> getListByCurrentSchoolyear()
    {
        var schoolyearDao = new SchoolyearDaoPostgres(context);
        var ids = await schoolyearDao.getCurrentAndNewSchoolyearIds();

        var semesterList = await context.Set<SemesterEntity>()
            .Where(e => e.schoolyear == ids.currentSchoolyear)
            .Select(e => e.semesterId)
            .ToListAsync();

        return await entities.Where(e => semesterList.Contains(e.semesterId)).ToListAsync();
    }

    public async Task<List<PartialEntity>> getListWithSubjectByEnrollment(string enrollmentId)
    {
        var partialList = await getListByCurrentSchoolyear();
        throw new NotImplementedException();
    }

    public async Task<List<PartialEntity>> getListByStudentId(string studentId)
    {
        var partials = await getListByCurrentSchoolyear();

        foreach (var partial in partials)
        {
            // this need refactor
            partial.grades = await context.Set<GradeEntity>()
                .Where(e => e.studentId == studentId && partial.partialId == 100000000)
                .AsNoTracking()
                .ToListAsync();
        }

        return partials;
    }
}