using wsmcbl.src.database.context;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.database;

public class StudentFileDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<StudentFileEntity, int>(context), IStudentFileDao
{
    public async Task updateAsync(StudentFileEntity? entity)
    {
        if (entity == null)
        {
            return;
        }

        var existingEntity = await getById(entity.fileId);

        if (existingEntity == null)
        {
            create(entity);
        }
        else
        {
            existingEntity.update(entity);
        }
    }
}