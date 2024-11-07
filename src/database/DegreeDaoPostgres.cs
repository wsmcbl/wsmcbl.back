using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.database;

public class DegreeDaoPostgres(PostgresContext context) : GenericDaoPostgres<DegreeEntity, string>(context), IDegreeDao
{
    public new async Task<DegreeEntity?> getById(string id)
    {
        var grade = await entities.Include(e => e.subjectList)
            .Include(e => e.enrollmentList)!
            .ThenInclude(e => e.subjectList)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.degreeId == id);

        if (grade == null)
        {
            throw new EntityNotFoundException("Grade", id);
        }

        return grade;
    }

    public void createList(List<DegreeEntity> gradeList)
    {
        foreach (var grade in gradeList)
        {
            create(grade);
        }
    }

    public async Task<List<DegreeEntity>> getValidListForTheSchoolyear()
    {
        var dao = new SchoolyearDaoPostgres(context);
        var ID = await dao.getCurrentAndNewSchoolyearIds();
        var schoolyearId = ID.newSchoolyear != "" ? ID.newSchoolyear : ID.currentSchoolyear;

        var list = await entities
            .Include(e => e.enrollmentList)
            .Where(e => e.schoolYear == schoolyearId).ToListAsync();

        return list.Where(e => e.enrollmentList!.Count != 0).ToList();
    }
}