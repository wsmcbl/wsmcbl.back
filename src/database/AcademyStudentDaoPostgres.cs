using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.database;

public class AcademyStudentDaoPostgres(PostgresContext context) : GenericDaoPostgres<StudentEntity, string>(context), IStudentDao
{
    public async Task<StudentEntity> getByIdAndSchoolyear(string studentId, string schoolyearId)
    {
        var result = await entities
            .Include(e => e.student)
            .FirstOrDefaultAsync(e => e.studentId == studentId && e.schoolYear == schoolyearId);

        if (result == null)
        {
            throw new EntityNotFoundException("AcademyStudent", studentId);
        }

        return result;
    }

    public async Task<StudentEntity> getByIdInCurrentSchoolyear(string studentId)
    {
        var schoolyear = DateTime.Today.Year.ToString();

        FormattableString query =
            $@"select s.* from academy.student s
               inner join secretary.schoolyear sy on sy.schoolyearid = s.schoolyear
               where sy.label = {schoolyear} and s.studentid = {studentId}";

        var result = await entities.FromSqlInterpolated(query)
            .Include(e => e.student)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        
        if (result == null)
        {
            throw new EntityNotFoundException("AcademyStudent", studentId);
        }

        return result;
    }
}