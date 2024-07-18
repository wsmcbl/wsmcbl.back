using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.database;

public class EnrollmentDaoPostgres(PostgresContext context) : GenericDaoPostgres<EnrollmentEntity, string>(context), IEnrollmentDao
{
    public new async Task<EnrollmentEntity?> getById(string id)
    {
        var entity = await entities
            .Include(e => e.studentList)
            .Include(e => e.subjectList)
            .FirstOrDefaultAsync(e => e.enrollmentId == id);

        if (entity == null)
        {
            throw new EntityNotFoundException("Enrollment", id);
        }
        
        return entity;
    }

    public new async Task<List<EnrollmentEntity>> getAll()
    {
        return await entities
            .Include(e => e.studentList)
            .Include(e => e.subjectList)
            .ToListAsync();
    }
}