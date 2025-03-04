using wsmcbl.src.database.context;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.config;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.database;

public class TariffDataDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<TariffDataEntity, string>(context), ITariffDataDao;

public class SubjectDataDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<SubjectDataEntity, string>(context), ISubjectDataDao;

public class TariffTypeDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<TariffTypeEntity, int>(context), ITariffTypeDao;

public class UserPermissionDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<UserPermissionEntity, string>(context), IUserPermissionDao;

public class RolePermissionDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<RolePermissionEntity, int>(context), IRolePermissionDao
{
    public void deleteItem(RolePermissionEntity item)
    {
        entities.Remove(item);
    }
}