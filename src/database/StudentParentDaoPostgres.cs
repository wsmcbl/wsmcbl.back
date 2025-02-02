using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.database;

public class StudentParentDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<StudentParentEntity, string>(context), IStudentParentDao
{
    public async Task updateAsync(StudentParentEntity? entity)
    {
        if (entity == null)
        {
            return;
        }

        if (entity.parentId == null)
        {
            create(entity);
            return;
        }

        var existingEntity = await getById(entity.parentId);
        if (existingEntity == null)
        {
            throw new EntityNotFoundException("TutorEntity", entity.parentId);
        }

        existingEntity.update(entity);
    }
}