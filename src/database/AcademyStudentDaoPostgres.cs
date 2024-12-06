using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.secretary;
using IStudentDao = wsmcbl.src.model.academy.IStudentDao;
using StudentEntity = wsmcbl.src.model.academy.StudentEntity;

namespace wsmcbl.src.database;

public class AcademyStudentDaoPostgres(PostgresContext context) : GenericDaoPostgres<StudentEntity, string>(context), IStudentDao
{
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

    public new async Task<StudentEntity?> getById(string studentId)
    {
        ISchoolyearDao schoolyearDao = new SchoolyearDaoPostgres(context);
        var ids = await schoolyearDao.getCurrentAndNewSchoolyearIds();

        var schoolyearId = ids.newSchoolyear != string.Empty ? ids.newSchoolyear : ids.currentSchoolyear;

        return await entities.FirstOrDefaultAsync(e => e.studentId == studentId && e.schoolYear == schoolyearId);
    }
}