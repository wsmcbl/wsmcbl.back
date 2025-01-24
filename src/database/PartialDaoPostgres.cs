using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.database;

public class PartialDaoPostgres(PostgresContext context) : GenericDaoPostgres<PartialEntity, int>(context), IPartialDao
{
    public async Task<List<PartialEntity>> getListByCurrentSchoolyear()
    {
        var daoFactory = new DaoFactoryPostgres(context);
        var schoolyear = await daoFactory.schoolyearDao.getCurrent(false);
        
        var semesterList = await context.Set<SemesterEntity>()
            .Where(e => e.schoolyear == schoolyear.id)
            .Select(e => e.semesterId)
            .ToListAsync();

        return await entities.Where(e => semesterList.Contains(e.semesterId)).ToListAsync();
    }

    public async Task<List<PartialEntity>> getListWithSubjectByEnrollment(string enrollmentId)
    {
        var partialList = await getListByCurrentSchoolyear();

        foreach (var item in partialList)
        {
            item.subjectPartialList = await context.Set<SubjectPartialEntity>()
                .Where(e => e.partialId == item.partialId && e.enrollmentId == enrollmentId)
                .Include(e => e.gradeList)
                .AsNoTracking()
                .ToListAsync();
        }

        return partialList;
    }
}