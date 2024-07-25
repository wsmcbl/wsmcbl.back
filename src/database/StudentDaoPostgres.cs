using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.database;

public class StudentDaoPostgres(PostgresContext context) : GenericDaoPostgres<StudentEntity, string>(context), IStudentDao
{
    public new async Task<StudentEntity?> getById(string id)
    {
        var result = await entities
            .Include(e => e.measurements)
            .Include(e => e.file)
            .Include(e => e.tutor)
            .Include(e => e.parents)
            .FirstOrDefaultAsync(e => e.studentId == id);

        if (result == null)
        {
            throw new EntityNotFoundException("Student", id);
        }

        return result;
    }
    
    public async Task<List<StudentEntity>> getAllWithSolvency()
    {
        return await entities.ToListAsync();
    }
}
