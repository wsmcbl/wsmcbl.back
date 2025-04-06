using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.database;

public class WithdrawnStudentDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<WithdrawnStudentEntity, int>(context), IWithdrawnStudentDao
{
    public async Task<List<WithdrawnStudentEntity>> getListByEnrollmentId(string enrollmentId)
    {
        return await entities.Where(e => e.lastEnrollmentId == enrollmentId).ToListAsync();
    }
}