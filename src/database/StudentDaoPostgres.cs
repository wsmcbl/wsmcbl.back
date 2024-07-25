using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.database;

public class StudentDaoPostgres(PostgresContext context) : GenericDaoPostgres<StudentEntity, string>(context), IStudentDao
{
    public new async Task<StudentEntity?> getById(string id)
    {
        var result = await entities.FirstOrDefaultAsync(e => e.studentId == id);

        if (result == null)
        {
            throw new EntityNotFoundException("Student", id);
        }

        result.tutor = await context.Set<StudentTutorEntity>().FirstOrDefaultAsync(e => e.studentId == id);
        result.measurements = await context.Set<StudentMeasurementsEntity>()
            .FirstOrDefaultAsync(e => e.studentId == id);
        result.file = await context.Set<StudentFileEntity>().FirstOrDefaultAsync(e => e.studentId == id);
        result.parents = await context.Set<StudentParentEntity>().Where(e => e.studentId == id).ToListAsync();

        return result;
    }
    
    public async Task<List<StudentEntity>> getAllWithSolvency()
    {
        return await entities.ToListAsync();
    }
}
