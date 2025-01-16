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
    {
        var enrollment = await context.Set<EnrollmentEntity>()
            .Where(e => e.enrollmentId == enrollmentId).FirstOrDefaultAsync();
        if (enrollment == null)
        {
            throw new EntityNotFoundException("Enrollment", enrollmentId);
        }
        
        if (enrollment.teacherId == null)
        {
            throw new EntityNotFoundException("Teacher", "null");
        }
        
        var teacher = await entities.Where(e => e.teacherId == enrollment.teacherId)
            .Include(e => e.user)
            .FirstOrDefaultAsync();

        if (teacher != null)
        {
            teacher.enrollment = enrollment;
        }
        
        return teacher;
    }

    public async Task<List<TeacherEntity>> getByListByIdList(List<string> value)
    {
        return await entities.Where(e => value.Contains(e.teacherId)).ToListAsync();
    }

    public async Task<TeacherEntity> getByUserId(Guid userId)
    {
        var result = await entities.Where(e => e.userId == userId).FirstOrDefaultAsync();
        if (result == null)
        {
            throw new EntityNotFoundException("TeacherEntity", userId.ToString());
        }

        return result;
    }
}