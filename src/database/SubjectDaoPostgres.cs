using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.database;

public class SubjectDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<SubjectEntity, string>(context), ISubjectDao
{
    public async Task<List<SubjectEntity>> getByEnrollmentId(string enrollmentId)
    {
        return await entities.Where(e => e.enrollmentId == enrollmentId)
            .Include(e => e.secretarySubject)
            .ToListAsync();
    }

    public async Task<SubjectEntity?> getBySubjectAndEnrollment(string subjectId, string enrollmentId)
    {
        return await entities.Where(e => e.subjectId == subjectId && e.enrollmentId == enrollmentId)
            .FirstOrDefaultAsync();
    }

    public async Task<List<SubjectEntity>> getListByTeacherId(string teacherId)
    {
        return await entities.Where(e => e.teacherId == teacherId).ToListAsync();
    }
}