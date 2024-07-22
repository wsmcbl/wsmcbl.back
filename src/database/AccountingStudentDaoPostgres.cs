using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.database;

public class AccountingStudentDaoPostgres(PostgresContext context) : GenericDaoPostgres<StudentEntity, string>(context), IStudentDao
{
    public new async Task<List<StudentEntity>> getAll()
    {
        var list = await entities.Include(d => d.student).ToListAsync();
        
        foreach (var item in list)
        {
            item.enrollmentLabel = await getEnrollmentLabel(item.studentId!);
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

        student.enrollmentLabel = await getEnrollmentLabel(student.studentId!);
        
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

    private async Task<string> getEnrollmentLabel(string studentId)
    {
        var academyStudent = await context.Set<model.academy.StudentEntity>()
            .FirstOrDefaultAsync(e => e.studentId == studentId);

        if (academyStudent == null)
        {
            return "";
        }
        
        var enrollment = await context.Set<model.academy.EnrollmentEntity>()
            .FirstOrDefaultAsync(e => e.enrollmentId == academyStudent.enrollmentId);

        return enrollment != null ? enrollment.label : "";
    }
}