using wsmcbl.src.database.context;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.config;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.database;

public class UserDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<UserEntity, string>(context), IUserDao;

public class TransactionDaoPostgres(PostgresContext context) 
    : GenericDaoPostgres<TransactionEntity, string>(context), ITransactionDao
{
    public override void create(TransactionEntity entity)
    {
        entity.computeTotal();
        base.create(entity);
    }
}
    
public class SecretaryStudentDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<model.secretary.StudentEntity, string>(context), model.secretary.IStudentDao;

public class TariffTypeDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<TariffTypeEntity, int>(context), ITariffTypeDao;

public class GradeDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<GradeEntity, int>(context), IGradeDao;