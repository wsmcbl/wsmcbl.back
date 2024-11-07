using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.database;

public class TeacherDaoPostgres(PostgresContext context) : GenericDaoPostgres<TeacherEntity, string>(context), ITeacherDao
{
    public new async Task<TeacherEntity?> getById(string id)
    {
        return await entities
            .Where(e => e.teacherId == id)
            .Include(e => e.user)
            .FirstOrDefaultAsync();
    }

    public new async Task<List<TeacherEntity>> getAll()
    {
        var result = await entities.Include(e => e.user).ToListAsync();

        if (result.Count == 0)
        {
            throw new InternalException("There is not teacher in the records.");
        }

        return result;
    }

    public async Task<TeacherEntity?> getByEnrollmentId(string enrollmentId)
    {////##############################################
        return await entities
            .Where(e => e.teacherId == enrollmentId)
            .Include(e => e.user)
            .Include(e => e.enrollment)
            .FirstOrDefaultAsync();
    }
}