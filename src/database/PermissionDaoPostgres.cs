using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.config;

namespace wsmcbl.src.database;

public class PermissionDaoPostgres(PostgresContext context) : GenericDaoPostgres<PermissionEntity, int>(context), IPermissionDao
{
    public async Task verifyIdListOrFail(List<int> permissionIdList)
    {
        var list = await getAll();
        var idList = list.Select(e => e.permissionId).ToList();

        var id = permissionIdList.FirstOrDefault(id => !idList.Contains(id));
        if (id != 0)
        {
            return;
        }
        
        throw new EntityNotFoundException("PermissionEntity", id.ToString());
    }
}