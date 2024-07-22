using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.config;
using wsmcbl.src.model.secretary;
using StudentEntity = wsmcbl.src.model.secretary.StudentEntity;

namespace wsmcbl.src.database;

public class TariffDataDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<TariffDataEntity, string>(context), ITariffDataDao;

public class SubjectDataDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<SubjectDataEntity, string>(context), ISubjectDataDao;

public class UserDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<UserEntity, string>(context), IUserDao;
public class TariffTypeDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<TariffTypeEntity, int>(context), ITariffTypeDao;

public class AcademyStudentDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<model.academy.StudentEntity, string>(context), model.academy.IStudentDao;




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

public class GradeDaoPostgres(PostgresContext context) : GenericDaoPostgres<GradeEntity, string>(context), IGradeDao
{
    public new async Task<GradeEntity?> getById(string id)
    {
        var grade = await entities.Include(e => e.enrollments)
            .Include(e => e.subjectList)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.gradeId == id);

        if (grade == null)
        {
            throw new EntityNotFoundException("Grade", id);
        }

        return grade;
    }

    public void createList(List<GradeEntity> gradeList)
    {
        foreach (var grade in gradeList)
        {
            create(grade);
        }
    }

    public async Task<List<GradeEntity>> getAllForTheCurrentSchoolyear()
    {
        throw new NotImplementedException();
    }
}



public class TeacherDaoPostgres(PostgresContext context) 
    : GenericDaoPostgres<TeacherEntity, string>(context), ITeacherDao 
{
    public new async Task<List<TeacherEntity>> getAll()
    {
        return await entities.Include(e => e.user).ToListAsync();
    }
}

    
public class StudentDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<model.secretary.StudentEntity, string>(context), model.secretary.IStudentDao
{
    public async Task<List<StudentEntity>> getAllWithSolvency()
    {
        throw new NotImplementedException();
    }
}

public class GradeDataDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<GradeDataEntity, string>(context), IGradeDataDao
{
    public new async Task<List<GradeDataEntity>> getAll()
    {
        return await entities.Include(e => e.subjectList).ToListAsync();
    }
}

public class SchoolyearDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<SchoolYearEntity, string>(context), ISchoolyearDao
{
    public async Task<SchoolYearEntity> getNewSchoolYear()
    {
        var year = getYear();

        var schoolYearEntity = await entities.FirstOrDefaultAsync(e => e.label == year.ToString());

        if (schoolYearEntity != null)
        {
            return schoolYearEntity;
        }
        
        schoolYearEntity = new SchoolYearEntity
        {
            label = year.ToString(),
            startDate = new DateOnly(year, 1, 1),
            deadLine = new DateOnly(year, 12, 31),
            isActive = true
        };
        
        create(schoolYearEntity);
        await context.SaveChangesAsync();

        return schoolYearEntity;
    }
    
    private static int getYear() => DateTime.Today.Month > 4 ? DateTime.Today.Year + 1 : DateTime.Today.Year;
}