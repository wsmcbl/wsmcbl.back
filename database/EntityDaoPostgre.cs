using wsmcbl.back.model.accounting;

namespace wsmcbl.back.database;

public class StudentDaoPostgre : GenericDaoPostgre<StudentEntity, string>, IStudentDao
{
    public StudentDaoPostgre(PostgresContext context) : base(context)
    {
    }
}