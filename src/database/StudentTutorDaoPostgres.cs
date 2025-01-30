using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.database;

public class StudentTutorDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<StudentTutorEntity, string>(context), IStudentTutorDao
{
    public async Task updateAsync(StudentTutorEntity? entity)
    {
        if (entity == null)
        {
            return;
        }

        var existingEntity = entity.isValidId() ? await getById(entity.tutorId!) : await getByInformation(entity);
        if (existingEntity == null)
        {
            create(entity);
        }
        else
        {
            existingEntity.update(entity);
        }
    }

    public async Task<StudentTutorEntity?> getByInformation(StudentTutorEntity tutor)
    {
        return await context.Set<StudentTutorEntity>()
            .FirstOrDefaultAsync(e =>
                tutor.name.Equals(e.name) && tutor.phone.Contains(e.phone));
    }

    public async Task<bool> hasOnlyOneStudent(string tutorId)
    {
        var studentList = await context.Set<StudentEntity>()
            .Where(e => e.tutorId == tutorId).ToListAsync();
        return studentList.Count >= 1;
    }
}