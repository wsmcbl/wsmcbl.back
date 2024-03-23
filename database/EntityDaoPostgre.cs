using Microsoft.EntityFrameworkCore;
using wsmcbl.back.model.accounting;

namespace wsmcbl.back.database;

public class StudentDaoPostgre(PostgresContext context) : GenericDaoPostgre<StudentEntity, string>(context), IStudentDao
{
    public new async Task<StudentEntity?> getById(string id)
    {
        return context.Student
            .Include(e => e.transactions)
            .FirstOrDefault(e => e.studentId == id);
    }
}
