using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;
using IStudentDao = wsmcbl.src.model.academy.IStudentDao;
using StudentEntity = wsmcbl.src.model.academy.StudentEntity;

namespace wsmcbl.src.database;

public class AcademyStudentDaoPostgres(PostgresContext context) : GenericDaoPostgres<StudentEntity, string>(context), IStudentDao
{
    private DaoFactory daoFactory { get; set; } = new DaoFactoryPostgres(context);
 
    public async Task<StudentEntity> getByIdInCurrentSchoolyear(string studentId)
    {
        var schoolyear = await daoFactory.schoolyearDao!.getCurrentOrNewSchoolyear();
        
        var result = await entities
            .FirstOrDefaultAsync(e => e.studentId == studentId && e.schoolYear == schoolyear.id);
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
        var schoolyear = await daoFactory.schoolyearDao!.getNewOrCurrentSchoolyear();
        return await entities
            .FirstOrDefaultAsync(e => e.studentId == studentId && e.schoolYear == schoolyear.id);
    }
}