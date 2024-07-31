using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.database;

public class EnrollmentDaoPostgres(PostgresContext context) : GenericDaoPostgres<EnrollmentEntity, string>(context), IEnrollmentDao
{
    public new async Task<List<EnrollmentEntity>> getAll()
    {
        return await entities
            .Include(e => e.studentList)
            .Include(e => e.subjectList)
            .ToListAsync();
    }
}