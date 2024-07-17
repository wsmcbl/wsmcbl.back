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

public class GradeDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<GradeEntity, int>(context), IGradeDao
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
}

public class EnrollmentDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<EnrollmentEntity, string>(context), IEnrollmentDao
{
    public new async Task<EnrollmentEntity?> getById(string id)
    {
        var entity = await entities
            .Include(e => e.students)
            .Include(e => e.subjects)
            .FirstOrDefaultAsync(e => e.enrollmentId == id);

        if (entity == null)
        {
            throw new EntityNotFoundException("Enrollment", id);
        }
        
        return entity;
    }

    public new async Task<List<EnrollmentEntity>> getAll()
    {
        return await entities
            .Include(e => e.students)
            .Include(e => e.subjects)
            .ToListAsync();
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