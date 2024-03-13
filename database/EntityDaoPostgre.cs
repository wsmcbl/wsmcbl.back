using wsmcbl.back.model.dao;
using wsmcbl.back.model.entity.academy;

namespace wsmcbl.back.database;

public class StudentDaoPostgre : GenericDaoPostgre<StudentEntity, string>, IStudentDao
{
    public StudentDaoPostgre(PostgresContext context) : base(context)
    {
    }

    public List<StudentEntity> getAll()
    {
        return context.Student.ToList();
    }
}