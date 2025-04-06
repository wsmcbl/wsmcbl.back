using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.database;

public class AcademySubjectDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<SubjectEntity, string>(context), ISubjectDao
{
    public async Task<List<SubjectEntity>> getByEnrollmentId(string enrollmentId)
    {
        return await entities.Where(e => e.enrollmentId == enrollmentId)
            .Include(e => e.secretarySubject)
            .ToListAsync();
    }

    public async Task<List<SubjectEntity>> getByEnrollmentId(string enrollmentId, int semester)
    {
        return await entities.Where(e => e.enrollmentId == enrollmentId)
            .Include(e => e.secretarySubject)
            .Where(e => e.secretarySubject!.semester == 3 || e.secretarySubject.semester == semester)
            .OrderBy(e => e.secretarySubject!.areaId)
            .ThenBy(e => e.secretarySubject!.number)
            .ToListAsync();
    }

    public async Task<SubjectEntity?> getBySubjectIdAndEnrollmentId(string subjectId, string enrollmentId)
    {
        return await entities.Where(e => e.subjectId == subjectId && e.enrollmentId == enrollmentId)
            .FirstOrDefaultAsync();
    }

    public async Task<List<SubjectEntity>> getListByTeacherId(string teacherId)
    {
        return await entities.Where(e => e.teacherId == teacherId).ToListAsync();
    }
}