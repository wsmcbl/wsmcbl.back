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

public class ExchangeRateDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<ExchangeRateEntity, int>(context), IExchangeRateDao
{
    public async Task<ExchangeRateEntity> getLastRate()
    {
        var schoolyearId = await getLastSchoolyearId();
        return await entities.Where(e => e.schoolyear == schoolyearId).FirstAsync();
    }

    private async Task<string> getLastSchoolyearId()
    {
        var schoolyearDao = new SchoolyearDaoPostgres(context);
        var ID = await schoolyearDao.getCurrentAndNewSchoolyearIds();

        return ID.newSchoolyear != "" ? ID.newSchoolyear : ID.currentSchoolyear;
    }
}

public class SubjectDaoPostgres(PostgresContext context) : GenericDaoPostgres<SubjectEntity, int>(context), ISubjectDao
{
    public async Task<List<SubjectEntity>> getByEnrollmentId(string enrollmentId)
    {
        return await entities.Where(e => e.enrollmentId == enrollmentId)
            .Include(e => e.secretarySubject)
            .ToListAsync();
    }

    public async Task<List<SubjectEntity>> getSubjectByTeacherAndEnrollment(string teacherId, string enrollmentId)
    {
        var enrollment = await context.Set<EnrollmentEntity>().Include(e => e.subjectList)
            .FirstOrDefaultAsync(e => e.enrollmentId == enrollmentId);

        return enrollment!.subjectList!.Where(e => e.teacherId == teacherId).ToList();
    }
}

public class SubjectPartialDaoPostgres(PostgresContext context) :
    GenericDaoPostgres<SubjectPartialEntity, int>(context), ISubjectPartialDao
{
    public async Task<List<SubjectPartialEntity>> getListByTeacherAndEnrollment(SubjectPartialEntity subjectPartial)
    {
        var result = await entities
            .Where(e => e.teacherId == subjectPartial.teacherId
                        && e.enrollmentId == subjectPartial.enrollmentId
                        && e.partialId == subjectPartial.partialId)
            .Include(e => e.gradeList)
            .ToListAsync();

        if (result.Count == 0)
        {
            throw new NotFoundException(
                $"There are no records of (SubjectPartialEntity) for the teacherId ({subjectPartial.teacherId})," +
                $" enrollmentId ({subjectPartial.enrollmentId})" +
                $" and partialId ({subjectPartial.partialId}).");
        }

        return result;
    }
}

public class StudentFileDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<StudentFileEntity, int>(context), IStudentFileDao
{
    public async Task updateAsync(StudentFileEntity? entity)
    {
        if (entity == null)
        {
            return;
        }

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
    public async Task updateAsync(StudentTutorEntity? entity)
    {
        if (entity == null)
        {
            return;
        }

        var existingEntity = await getById(entity.tutorId!);

        if (existingEntity == null)
        {
            create(entity);
        }
        else
        {
            existingEntity.update(entity);
        }
    }

    public async Task<StudentTutorEntity?> getByInformation(StudentTutorEntity tutor)
    {
        return await context.Set<StudentTutorEntity>()
            .FirstOrDefaultAsync(e =>
                tutor.name.Equals(e.name) && tutor.phone.Contains(e.phone));
    }
}

public class StudentParentDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<StudentParentEntity, string>(context), IStudentParentDao
{
    public async Task updateAsync(StudentParentEntity? entity)
    {
        if (entity == null)
        {
            return;
        }

        if (entity.parentId == null)
        {
            create(entity);
            return;
        }

        var existingEntity = await getById(entity.parentId);

        if (existingEntity == null)
        {
            throw new EntityNotFoundException("Tutor", entity.parentId);
        }

        existingEntity.update(entity);
    }
}

public class StudentMeasurementsDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<StudentMeasurementsEntity, int>(context), IStudentMeasurementsDao
{
    public async Task updateAsync(StudentMeasurementsEntity? entity)
    {
        if (entity == null)
        {
            return;
        }

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
            throw new IncorrectDataBadRequestException("Transaction");
        }

        entity.computeTotal();
        base.create(entity);
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

public class SemesterDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<SemesterEntity, int>(context), ISemesterDao
{
    public async Task<List<SemesterEntity>> getAllOfCurrentSchoolyear()
    {
        var schoolyear = DateTime.Today.Year.ToString();

        FormattableString query = $@"select s.* from academy.semester s
               inner join secretary.schoolyear sy on sy.schoolyearid = s.schoolyear
               where sy.label = {schoolyear}";

        var result = await entities.FromSqlInterpolated(query)
            .AsNoTracking()
            .ToListAsync();

        return result;
    }
}

public class GradeDaoPostgres(PostgresContext context) : GenericDaoPostgres<GradeEntity, int>(context), IGradeDao
{
    public async Task addRange(SubjectPartialEntity subjectPartial, List<GradeEntity> gradeList)
    {
        var subjectPartialIdsList = await context.Set<SubjectPartialEntity>()
            .Where(e => e.enrollmentId == subjectPartial.enrollmentId
                        && e.teacherId == subjectPartial.teacherId
                        && e.partialId == subjectPartial.partialId)
            .Select(e => e.subjectPartialId)
            .ToListAsync();

        var currentGradeList = await entities
            .Where(e => subjectPartialIdsList.Contains(e.subjectPartialId))
            .ToListAsync();

        foreach (var currentGrade in currentGradeList)
        {
            var newGrade = gradeList.First(e => e.gradeId == currentGrade.gradeId);
            currentGrade.updateGrades(newGrade.grade, newGrade.conductGrade);
        }
    }
}