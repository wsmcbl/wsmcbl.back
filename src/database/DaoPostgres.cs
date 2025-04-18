using wsmcbl.src.database.context;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.config;
using wsmcbl.src.model.secretary;
using ISubjectDao = wsmcbl.src.model.secretary.ISubjectDao;
using SubjectEntity = wsmcbl.src.model.secretary.SubjectEntity;

namespace wsmcbl.src.database;

public class TariffDataDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<TariffDataEntity, int>(context), ITariffDataDao;

public class SubjectDataDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<SubjectDataEntity, int>(context), ISubjectDataDao;

public class SubjectAreaDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<SubjectAreaEntity, int>(context), ISubjectAreaDao;

public class TariffTypeDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<TariffTypeEntity, int>(context), ITariffTypeDao;

public class UserPermissionDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<UserPermissionEntity, string>(context), IUserPermissionDao;

public class RolePermissionDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<RolePermissionEntity, int>(context), IRolePermissionDao;