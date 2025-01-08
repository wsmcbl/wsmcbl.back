using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.config;

namespace wsmcbl.src.database;

public class UserDaoPostgres(PostgresContext context) : GenericDaoPostgres<UserEntity, Guid>(context), IUserDao
{
    public async Task<UserEntity> getById(string userId)
    {
        UserEntity? user = null;
        if (Guid.TryParse(userId, out var userIdGuid))
        {
            user = await entities
                .Include(e => e.role)
                .FirstOrDefaultAsync(e => e.userId == userIdGuid);
        }

        if (user == null)
        {
            throw new EntityNotFoundException("User", userId);
        }

        return user;
    }

    public async Task<UserEntity> getUserByEmail(string email)
    {
        var result = await entities.Include(e => e.role)
            .Include(e => e.permissionList)
            .FirstOrDefaultAsync(e => e.email == email);

        if (result == null)
        {
            throw new EntityNotFoundException($"User with email ({email}) not found.");
        }

        return result;
    }

    public async Task<bool> isEmailDuplicate(string email)
    {
        var result = await entities.FirstOrDefaultAsync(e => e.email == email);
        return result != null;
    }

    public async Task isUserDuplicate(UserEntity user)
    {
        var result = await entities
            .Where(e => user.name.Trim().Equals(e.name.Trim()) &&
                        user.secondName == e.secondName &&
                        user.surname.Trim().Equals(e.surname.Trim()) &&
                        user.secondSurname == e.secondSurname).FirstOrDefaultAsync();

        if (result != null)
        {
            throw new ConflictException("User already exists (duplicate).");
        }
    }
}