using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.database;

public class StudentDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<StudentEntity, string>(context), IStudentDao
{
    
    public async Task<StudentEntity> getByIdWithProperties(string id)
    {
        var entity = await entities.FirstOrDefaultAsync(e => e.studentId == id);

        if (entity == null)
        {
            throw new EntityNotFoundException("Student", id);
        }

        var tutor = await context.Set<StudentTutorEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.studentId == id);

        if (tutor == null)
        {
            throw new EntityNotFoundException($"Entity tutor of Student with ID = {id} not found.");
        }

        entity.tutor = tutor;

        entity.measurements = await context.Set<StudentMeasurementsEntity>().AsNoTracking()
            .FirstOrDefaultAsync(e => e.studentId == id);

        entity.file = await context.Set<StudentFileEntity>().AsNoTracking()
            .FirstOrDefaultAsync(e => e.studentId == id);

        entity.parents = await context.Set<StudentParentEntity>()
            .Where(e => e.studentId == id)
            .AsNoTracking()
            .ToListAsync();

        return entity;
    }

    private const int ENROLLMENT_TARIFF = 4;

    public async Task<List<StudentEntity>> getAllWithSolvency()
    {
        var schoolyear = await new SchoolyearDaoPostgres(context).getSchoolYearByLabel(DateTime.Today.Year);

        var tariff = await context.Set<model.accounting.TariffEntity>()
            .Where(e => e.schoolYear == schoolyear.id)
            .Where(e => e.type == ENROLLMENT_TARIFF)
            .FirstOrDefaultAsync();

        if (tariff == null)
        {
            throw new EntityNotFoundException("tariff", $"(type) {ENROLLMENT_TARIFF}");
        }

        FormattableString query = $@"
            select s.* from secretary.student s
            inner join accounting.debthistory d on d.studentid = s.studentid
            where d.tariffid = {tariff.tariffId} and (d.debtbalance / d.amount) > 0.45;";

         return await entities.FromSqlInterpolated(query).AsNoTracking().ToListAsync();
    }

    public async Task updateAsync(StudentEntity? entity)
    {
        if (entity == null)
        {
            return;
        }
        
        var existingStudent = await getById(entity.studentId!);
        
        if (existingStudent == null)
        {
            throw new EntityNotFoundException("StudentEntity", entity.studentId);
        }
        
        existingStudent.update(entity);
        update(existingStudent);
    }
}