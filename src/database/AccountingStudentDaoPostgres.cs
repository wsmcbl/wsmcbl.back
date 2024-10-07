using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.database;

public class AccountingStudentDaoPostgres(PostgresContext context) : GenericDaoPostgres<StudentEntity, string>(context), IStudentDao
{
    public new async Task<List<StudentEntity>> getAll()
    {
        FormattableString query = $@"select std.* from accounting.student std 
        join secretary.student as sec_std on sec_std.studentid = std.studentid
        where sec_std.studentstate = 'true'";

        var list = await context.Set<StudentEntity>()
            .FromSqlInterpolated(query)
            .AsNoTracking()
            .Include(e => e.student)
            .ThenInclude(e => e.tutor)
            .ToListAsync();
        
        foreach (var item in list)
        {
            await setEnrollmentLabel(item);
        }

        return list;
    }

    public new async Task<StudentEntity?> getById(string id)
    {
        var student = await entities
            .Include(e => e.discount)
            .Include(e => e.student)
            .Include(e => e.transactions)!
            .ThenInclude(t => t.details)
            .FirstOrDefaultAsync(e => e.studentId == id);
        
        if (student is null)
        {
            throw new EntityNotFoundException("Student", id);
        }

        await setEnrollmentLabel(student);
        
        foreach (var transaction in student.transactions!)
        {
            foreach (var item in transaction.details)
            {
                var tariff = await context.Set<TariffEntity>().FirstOrDefaultAsync(t => t.tariffId == item.tariffId);
                item.setTariff(tariff);
            }
        }
        
        return student;
    }

    private async Task setEnrollmentLabel(StudentEntity student)
    {
        var sql = @"SELECT enroll.label as EnrollmentLabel
                       FROM academy.student as std
                       JOIN academy.enrollment as enroll ON enroll.enrollmentid = std.enrollmentid
                       WHERE std.studentid = @p0";

        student.enrollmentLabel = await context.Database
            .SqlQueryRaw<string>(sql, student.studentId!)
            .FirstOrDefaultAsync();
    }
}