using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.config;

namespace wsmcbl.src.database;

public class MediaDaoPostgres(PostgresContext context) : GenericDaoPostgres<MediaEntity, int>(context), IMediaDao
{
    public async Task<string> getByTypeAndSchoolyear(int type, string schoolyearId)
    {
        var result = await entities.Where(e => e.type == type)
            .Where(e => e.schoolyearId == schoolyearId)
            .FirstOrDefaultAsync();

        if (result == null)
        {
            throw new EntityNotFoundException($"Media with type ({type}) and schoolyearid ({schoolyearId}) not found.");
        }

        return result.value;
    }
}