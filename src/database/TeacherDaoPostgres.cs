using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.database;

public class TeacherDaoPostgres(PostgresContext context) : GenericDaoPostgres<TeacherEntity, string>(context), ITeacherDao
{
    public new async Task<TeacherEntity?> getById(string id)
    {
        return await entities.Include(e => e.user).FirstOrDefaultAsync();
    }

    public new async Task<List<TeacherEntity>> getAll()
    {
        return await entities.Include(e => e.user).ToListAsync();
    }

    public async Task<TeacherEntity?> getByEnrollmentId(string enrollmentId)
    {
        return await entities
            .Where(e => e.enrollmentId == enrollmentId)
            .Include(e => e.user)
            .Include(e => e.enrollment)
            .FirstOrDefaultAsync();
    }
}