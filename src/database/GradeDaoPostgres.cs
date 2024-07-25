using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.database;

public class GradeDaoPostgres(PostgresContext context) : GenericDaoPostgres<GradeEntity, string>(context), IGradeDao
{
    public new async Task<GradeEntity?> getById(string id)
    {
        var grade = await entities.Include(e => e.enrollments)
            .Include(e => e.subjectList)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.gradeId == id);

        if (grade == null)
        {
            throw new EntityNotFoundException("Grade", id);
        }

        return grade;
    }

    public void createList(List<GradeEntity> gradeList)
    {
        foreach (var grade in gradeList)
        {
            create(grade);
        }
    }

    public async Task<List<GradeEntity>> getAllForTheCurrentSchoolyear()
    {
        var dao = new SchoolyearDaoPostgres(context);
        var currentSchoolyear = await dao.getSchoolYearByLabel(DateTime.Today.Year);

        var list = entities
            .Where(e => e.schoolYear == currentSchoolyear.id)
            .Include(e => e.enrollments);

        return await list.ToListAsync();
    }
}