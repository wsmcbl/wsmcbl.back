using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.database;

public class StudentDaoPostgres(PostgresContext context) : GenericDaoPostgres<StudentEntity, string>(context), IStudentDao
{
    public new async Task<List<StudentEntity>> getAll()
    {
        return await entities.Include(d => d.student).ToListAsync();
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
        
        foreach (var transaction in student.transactions!)
        {
            foreach (var item in transaction.details)
            {
                var tariff = await context.Tariff.FirstOrDefaultAsync(t => t.tariffId == item.tariffId);
                item.setTariff(tariff);
            }
        }
        
        return student;
    }
}