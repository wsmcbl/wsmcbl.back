using Microsoft.EntityFrameworkCore;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.database.context;

public class EnrollmentDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<EnrollmentEntity, string>(context), IEnrollmentDao
{
    public new async Task<EnrollmentEntity?> getById(string id)
    {
        var entity = await entities
            .Include(e => e.students)
            .Include(e => e.subjects)
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
            .Include(e => e.students)
            .Include(e => e.subjects)
            .ToListAsync();
    }

    public async Task createEnrollments(int gradeId, int quantity)
    {
        throw new NotImplementedException();
    }
}