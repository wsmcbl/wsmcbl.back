using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.config;

namespace wsmcbl.src.database;

public class PermissionDaoPostgres(PostgresContext context) : GenericDaoPostgres<PermissionEntity, int>(context), IPermissionDao
{
    public async Task verifyIdListOrFail(List<int> permissionIdList)
    {
        var list = (await getAll()).Select(e => e.permissionId).ToList();
        if (permissionIdList.Count(item => list.Contains(item)) == permissionIdList.Count)
        {
            return;
        }
        
        throw new EntityNotFoundException("PermissionEntity", $"{permissionIdList.FirstOrDefault(id => !list.Contains(id))}");
    }
}