using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.config;
using wsmcbl.src.model.secretary;
using ISubjectDao = wsmcbl.src.model.secretary.ISubjectDao;
using SubjectEntity = wsmcbl.src.model.secretary.SubjectEntity;

namespace wsmcbl.src.database;

public class TariffDataDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<TariffDataEntity, string>(context), ITariffDataDao;

public class SubjectDataDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<SubjectDataEntity, string>(context), ISubjectDataDao;

public class SchoolyearDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<SchoolYearEntity, string>(context), ISchoolyearDao;

public class AcademySubjectDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<model.academy.SubjectEntity, string>(context), model.academy.ISubjectDao;

public class UserDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<UserEntity, string>(context), IUserDao;
    
public class SecretaryStudentDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<model.secretary.StudentEntity, string>(context), model.secretary.IStudentDao;

public class TariffTypeDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<TariffTypeEntity, int>(context), ITariffTypeDao;

public class SubjectDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<SubjectEntity, string>(context), ISubjectDao;

public class TransactionDaoPostgres(PostgresContext context) 
    : GenericDaoPostgres<TransactionEntity, string>(context), ITransactionDao
{
    public override void create(TransactionEntity entity)
    {
        if (!entity.checkData())
        {
            throw new IncorrectDataException("transaction");
        }
        
        entity.computeTotal();
        base.create(entity);
    }
}

public class GradeDaoPostgres(PostgresContext context) : GenericDaoPostgres<GradeEntity, int>(context), IGradeDao
{
    public new async Task<List<GradeEntity>> getAll()
    {
        var list = await entities
            .Include(e => e.enrollments)
            .ToListAsync();
        
        foreach (var item in list)
        {
            item.computeQuantity();
        }

        return list;
    }

    public void createList(List<GradeEntity> gradeList)
    {
        foreach (var grade in gradeList)
        {
            create(grade);
        }
    }
}



public class TeacherDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<TeacherEntity, string>(context), ITeacherDao
{
    public new async Task<TeacherEntity?> getById(string id)
    {
        return await entities.Include(e => e.subjects)
            .FirstOrDefaultAsync(e => e.teacherId == id);
    }
}