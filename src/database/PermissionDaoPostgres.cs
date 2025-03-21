using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.config;

namespace wsmcbl.src.database;

public class PermissionDaoPostgres(PostgresContext context) : GenericDaoPostgres<PermissionEntity, int>(context), IPermissionDao
{
    public async Task verifyIdListOrFail(List<int> permissionIdList)
    {
        var list = await getAll();
        var listId = list.Select(e => e.permissionId).ToList();

        foreach (var id in permissionIdList.Where(id => !listId.Contains(id)))
        {
            throw new EntityNotFoundException("PermissionEntity", id.ToString());
        }
    }
}