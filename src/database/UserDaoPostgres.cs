using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.config;

namespace wsmcbl.src.database;

public class UserDaoPostgres(PostgresContext context) : GenericDaoPostgres<UserEntity, string>(context), IUserDao
{
    public async Task<UserEntity> getUserByEmail(string email)
    {
        var result = await entities.FirstOrDefaultAsync(e => e.email == email);

        if (result == null)
        {
            throw new EntityNotFoundException($"User with email ({email}) not found.");
        }

        return result;
    }
}