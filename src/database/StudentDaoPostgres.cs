using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model;
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
            .FirstOrDefaultAsync(e => e.tutorId == entity.tutorId);

        if (tutor == null)
        {
            throw new EntityNotFoundException($"Entity of type (Tutor) with StudentId ({id}) not found.");
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

    public async Task<StudentEntity?> getByInformation(StudentEntity student)
    {
        return (await context.Set<StudentEntity>().Where(e => student.name == e.name).ToListAsync())
            .Find(e => student.getStringData().Equals(e.getStringData()));
    }

    public async Task<List<StudentEntity>> getAllWithSolvency()
    {
        var schoolyear = await new SchoolyearDaoPostgres(context).getSchoolYearByLabel(DateTime.Today.Year);

        var tariffList = await context.Set<model.accounting.TariffEntity>()
            .Where(e => e.schoolYear == schoolyear.id)
            .Where(e => e.type == Const.TARIFF_REGISTRATION).ToListAsync();

        if (tariffList.Count == 0)
        {
            throw new EntityNotFoundException(
                $"Entities of type (Tariff) with type ({Const.TARIFF_REGISTRATION}) not found.");
        }

        var tariffsId = string.Join(" OR ", tariffList.Select(item => $"d.tariffid = {item.tariffId}"));
        var query = $@"SELECT s.* FROM secretary.student s
                    INNER JOIN accounting.debthistory d ON d.studentid = s.studentid
                    WHERE ({tariffsId}) AND (d.debtbalance / d.amount) > 0.45;";
        
        return await entities.FromSqlRaw(query).AsNoTracking().ToListAsync();
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