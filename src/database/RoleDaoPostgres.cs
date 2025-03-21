using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.model.config;

namespace wsmcbl.src.database;

public class RoleDaoPostgres(PostgresContext context) : GenericDaoPostgres<RoleEntity, int>(context), IRoleDao
{
    public override async Task<RoleEntity?> getById(int id)
    {
        return await entities.Include(e => e.rolePermissionList)
            .ThenInclude(e => e.permission)
            .FirstOrDefaultAsync(e => e.roleId == id);
    }
}