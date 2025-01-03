using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using IStudentDao = wsmcbl.src.model.academy.IStudentDao;
using StudentEntity = wsmcbl.src.model.academy.StudentEntity;

namespace wsmcbl.src.database;

public class AcademyStudentDaoPostgres(PostgresContext context) : GenericDaoPostgres<StudentEntity, string>(context), IStudentDao
{
    public async Task<StudentEntity> getByIdInCurrentSchoolyear(string studentId)
    {
        var schoolyearDao = new SchoolyearDaoPostgres(context);
        var ids = await schoolyearDao.getCurrentAndNewSchoolyearIds();
        
        var result = await entities
            .FirstOrDefaultAsync(e => e.studentId == studentId && e.schoolYear == ids.currentSchoolyear);
        if (result == null)
        {
            throw new EntityNotFoundException("AcademyStudent", studentId);
        }

        return result;
    }

    public async Task updateEnrollment(string studentId, string enrollmentId)
    {
        FormattableString query =
            $@"update academy.student set enrollmentid = {enrollmentId} where studentid = {studentId};";
        await context.Database.ExecuteSqlAsync(query);
    }

    public async Task<bool> hasAEnroll(string studentId)
    {
        try
        {
            await getByIdInCurrentSchoolyear(studentId);
            return true;
        }
        catch (EntityNotFoundException)
        {
            return false;
        }
    }

    public new async Task<StudentEntity?> getById(string studentId)
    {
        var schoolyearDao = new SchoolyearDaoPostgres(context);
        var ids = await schoolyearDao.getCurrentAndNewSchoolyearIds();

        var schoolyearId = ids.newSchoolyear != string.Empty ? ids.newSchoolyear : ids.currentSchoolyear;

        return await entities.FirstOrDefaultAsync(e => e.studentId == studentId && e.schoolYear == schoolyearId);
    }
}