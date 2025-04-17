using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.database;


public class SubjectDaoPostgres(PostgresContext context) : GenericDaoPostgres<SubjectEntity, string>(context), ISubjectDao
{
    public async Task<List<SubjectEntity>> getListForCurrentSchoolyearByLevel(int level)
    {
        var daoFactory = new DaoFactoryPostgres(context);
        var schoolyear = await daoFactory.schoolyearDao.getCurrent();

        var label = level == 1 ? "Primaria" : "Secundaria";
        return await entities.AsNoTracking()
            .GroupJoin(
                context.Set<DegreeEntity>().Where(e => e.schoolyearId == schoolyear.id && e.educationalLevel == label),
                s => s.degreeId,
                g => g.degreeId,
                (std, gradeList) => std)
            .ToListAsync();
    }
}