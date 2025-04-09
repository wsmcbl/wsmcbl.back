using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.database;

public class WithdrawnStudentDaoPostgres(PostgresContext context) : GenericDaoPostgres<WithdrawnStudentEntity, int>(context), IWithdrawnStudentDao
{
    public async Task<List<WithdrawnStudentEntity>> getListByEnrollmentId(string enrollmentId, bool hasBeforeFirstPartial = false)
    {
        var query = entities.Where(e => e.lastEnrollmentId == enrollmentId);

        if (!hasBeforeFirstPartial)
        {
            return await query.Include(e => e.student).ToListAsync();
        }

        var daoFactory = new DaoFactoryPostgres(context);
        var partialList = await daoFactory.partialDao.getListForCurrentSchoolyear();

        var firstPartial = partialList.FirstOrDefault(e => e is { semester: 1, partial: 1 });
        if (firstPartial == null)
        {
            throw new ConflictException("There is not initial partial.");
        }

        return await query.Where(e => firstPartial.startDate >= DateOnly.FromDateTime(e.enrolledAt))
            .Include(e => e.student).ToListAsync();
    }

    public async Task<List<WithdrawnStudentEntity>> getAllForCurrentSchoolyear()
    {
        var daoFactory = new DaoFactoryPostgres(context);
        var currentSchoolyear = await daoFactory.schoolyearDao.getCurrent();

        return await entities.Where(e => e.schoolyearId == currentSchoolyear.id).ToListAsync();
    }
}