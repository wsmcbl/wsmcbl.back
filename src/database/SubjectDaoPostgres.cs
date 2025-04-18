using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.database;


public class SubjectDaoPostgres(PostgresContext context) : GenericDaoPostgres<SubjectEntity, string>(context), ISubjectDao
{
    public async Task<List<SubjectEntity>> getListForCurrentSchoolyearByLevel(int level, int semester)
    {
        var daoFactory = new DaoFactoryPostgres(context);
        var schoolyear = await daoFactory.schoolyearDao.getCurrent();

        var label = level == 2 ? "Primaria" : "Secundaria";
        return await entities.AsNoTracking()
            .Where(e => e.semester == 3 || e.semester == semester)
            .Where(sub => context.Set<DegreeEntity>()
                .Any(e => e.educationalLevel == label && e.degreeId == sub.degreeId && e.schoolyearId == schoolyear.id))
            .ToListAsync();
    }
}