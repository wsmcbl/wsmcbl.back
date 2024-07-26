using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.database;

public class StudentDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<StudentEntity, string>(context), IStudentDao
{
    public async Task updateAsync(StudentEntity entity)
    {
        var existingStudent = await getById(entity.studentId!);
        existingStudent!.update(entity);
        update(existingStudent);
    }

    public new async Task<StudentEntity?> getById(string id)
    {
        var result = await entities.FirstOrDefaultAsync(e => e.studentId == id);

        if (result == null)
        {
            throw new EntityNotFoundException("Student", id);
        }

        result.tutor = await context.Set<StudentTutorEntity>().FirstOrDefaultAsync(e => e.studentId == id);

        result.measurements = await context.Set<StudentMeasurementsEntity>()
            .FirstOrDefaultAsync(e => e.studentId == id);

        result.file = await context.Set<StudentFileEntity>().FirstOrDefaultAsync(e => e.studentId == id);

        result.parents = await context.Set<StudentParentEntity>().Where(e => e.studentId == id).ToListAsync();

        return result;
    }

    public async Task<List<StudentEntity>> getAllWithSolvency()
    {
        var schoolyear = await (new SchoolyearDaoPostgres(context))
            .getSchoolYearByLabel(DateTime.Today.Year);

        var tariff = await context.Set<model.accounting.TariffEntity>()
            .Where(e => e.schoolYear == schoolyear.id)
            .Where(e => e.type == 4)
            .FirstOrDefaultAsync();

        if (tariff == null)
        {
            throw new EntityNotFoundException("tariff", "(type) 10");
        }

        FormattableString query =
            $@"select s.* from secretary.student s
            inner join accounting.debthistory d on d.studentid = s.studentid
            where d.tariffid = {tariff.tariffId} and (d.debtbalance / d.amount) > 0.45;";

        var list = await entities.FromSqlInterpolated(query).AsNoTracking().ToListAsync();

        return list;
    }
}