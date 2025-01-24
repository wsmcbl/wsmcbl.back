using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.database;

public class DegreeDaoPostgres(PostgresContext context) : GenericDaoPostgres<DegreeEntity, string>(context), IDegreeDao
{
    public new async Task<DegreeEntity?> getById(string id)
    {
        var degree = await entities.Include(e => e.subjectList)
            .Include(e => e.enrollmentList)!
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.degreeId == id);

        if (degree == null)
        {
            throw new EntityNotFoundException("GradeEntity", id);
        }

        return degree;
    }
    
    public async Task<DegreeEntity?> getWithAllPropertiesById(string id)
    {
        var query = entities.Include(e => e.subjectList)
            .Include(e => e.enrollmentList)!
            .ThenInclude(e => e.studentList)!
                .ThenInclude(e => e.student)
            .Include(e => e.enrollmentList)!
                .ThenInclude(e => e.subjectList)
            .AsNoTracking();

        var degree = await query.FirstOrDefaultAsync(e => e.degreeId == id);
        if (degree == null)
        {
            throw new EntityNotFoundException("GradeEntity", id);
        }

        return degree;
    }    

    public async Task createRange(List<DegreeEntity> degreeList)
    {
        entities.AddRange(degreeList);
        await saveAsync();
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

    public async Task<List<DegreeEntity>> getAll(string schoolyearId)
    {
        return await entities
            .Include(e => e.enrollmentList)
            .Where(e => e.schoolYear == schoolyearId).ToListAsync();
    }

    public async Task<DegreeEntity?> getByEnrollmentId(string enrollmentId)
    {
        var enrollment = await context.Set<EnrollmentEntity>()
            .FirstOrDefaultAsync(e => e.enrollmentId == enrollmentId);
        if (enrollment == null)
        {
            throw new EntityNotFoundException("EnrollmentEntity", enrollmentId);
        }

        return await entities.FirstOrDefaultAsync(e => e.degreeId == enrollment.degreeId);
    }
}