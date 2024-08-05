using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.config;
using wsmcbl.src.model.secretary;
using SubjectEntity = wsmcbl.src.model.academy.SubjectEntity;

namespace wsmcbl.src.database;

public class TariffDataDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<TariffDataEntity, string>(context), ITariffDataDao;

public class SubjectDataDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<SubjectDataEntity, string>(context), ISubjectDataDao;

public class UserDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<UserEntity, string>(context), IUserDao;

public class TariffTypeDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<TariffTypeEntity, int>(context), ITariffTypeDao;

public class PartialDaoPostgres(PostgresContext context) : GenericDaoPostgres<PartialEntity, int>(context), IPartialDao
{
    public async Task<List<PartialEntity>> getListByCurrentSchoolyear()
    {
        var daoAux = new SchoolyearDaoPostgres(context);
        var schoolyear = await daoAux.getCurrentSchoolYear();
       
        FormattableString query =
            $@"select p.* from academy.partial p
               inner join academy.semester s on p.semesterid = s.semesterid
               where s.schoolyear = {schoolyear.id}";

        var partials = await context.Set<PartialEntity>()
            .FromSqlInterpolated(query)
            .Include(e => e.grades)
            .AsNoTracking()
            .ToListAsync();

        return partials;
    }
}

public class AcademyStudentDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<model.academy.StudentEntity, string>(context), model.academy.IStudentDao
{
    public async Task<model.academy.StudentEntity> getByIdAndSchoolyear(string studentId, string schoolyearId)
    {
        var result = await entities
            .Include(e => e.student)
            .FirstOrDefaultAsync(e => e.studentId == studentId && e.schoolYear == schoolyearId);

        if (result == null)
        {
            throw new EntityNotFoundException("Academy Student", studentId);
        }

        return result;
    }
}

public class SubjectDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<SubjectEntity, int>(context), ISubjectDao
{
    public async Task<List<SubjectEntity>> getByEnrollmentId(string enrollmentId)
    {
        return await entities.Where(e => e.enrollmentId == enrollmentId)
            .ToListAsync();
    }
}

public class StudentFileDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<StudentFileEntity, int>(context), IStudentFileDao
{
    public async Task updateAsync(StudentFileEntity entity)
    {
        var existingEntity = await getById(entity.fileId);

        if (existingEntity == null)
        {
            create(entity);
        }
        else
        {
            existingEntity.update(entity);
        }
    }
}

public class StudentTutorDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<StudentTutorEntity, string>(context), IStudentTutorDao
{
    public async Task updateAsync(StudentTutorEntity entity)
    {
        var existingEntity = await getById(entity.tutorId!);

        if (existingEntity == null)
        {
            entity.studentId = "";
            create(entity);
        }
        else
        {
            existingEntity.update(entity);
        }
    }
}

public class StudentParentDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<StudentParentEntity, string>(context), IStudentParentDao
{
    public async Task updateAsync(StudentParentEntity entity)
    {
        var existingEntity = await getById(entity.parentId);

        if (existingEntity == null)
        {
            entity.parentId = "";
            create(entity);
        }
        else
        {
            existingEntity.update(entity);
        }
    }
}

public class StudentMeasurementsDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<StudentMeasurementsEntity, int>(context), IStudentMeasurementsDao
{
    public async Task updateAsync(StudentMeasurementsEntity entity)
    {
        var existingEntity = await getById(entity.measurementId);

        if (existingEntity == null)
        {
            create(entity);
        }
        else
        {
            existingEntity.update(entity);
        }
    }
}

public class TransactionDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<TransactionEntity, string>(context), ITransactionDao
{
    public override void create(TransactionEntity entity)
    {
        if (!entity.checkData())
        {
            throw new IncorrectDataBadRequestException("transaction");
        }

        entity.computeTotal();
        base.create(entity);
    }
}

public class TeacherDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<TeacherEntity, string>(context), ITeacherDao
{
    public new async Task<List<TeacherEntity>> getAll()
    {
        return await entities.Include(e => e.user).ToListAsync();
    }

    public async Task<TeacherEntity> getByEnrollmentId(string enrollmentId)
    {
        var result = await entities
            .Where(e => e.enrollmentId == enrollmentId)
            .Include(e => e.user)
            .Include(e => e.enrollment)
            .FirstOrDefaultAsync();

        if (result == null)
        {
            throw new EntityNotFoundException($"Teacher with EnrollmentId = {enrollmentId}, not found.");
        }

        return result;
    }
}

public class DegreeDataDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<DegreeDataEntity, string>(context), IDegreeDataDao
{
    public new async Task<List<DegreeDataEntity>> getAll()
    {
        return await entities.Include(e => e.subjectList).ToListAsync();
    }
}