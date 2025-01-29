using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.dao;
using IStudentDao = wsmcbl.src.model.academy.IStudentDao;
using StudentEntity = wsmcbl.src.model.academy.StudentEntity;

namespace wsmcbl.src.database;

public class AcademyStudentDaoPostgres : GenericDaoPostgres<StudentEntity, string>, IStudentDao
{
    private DaoFactory daoFactory { get; set; }

    public AcademyStudentDaoPostgres(PostgresContext context) : base(context)
    {
        daoFactory = new DaoFactoryPostgres(context);
    }

    public async Task<bool> hasAEnroll(string studentId)
    {
        try
        {
            await getCurrentById(studentId);
            return true;
        }
        catch (EntityNotFoundException)
        {
            return false;
        }
    }

    public new async Task<StudentEntity?> getById(string studentId)
    {
        var schoolyear = await daoFactory.schoolyearDao!.getNewOrCurrent();
        return await getById(studentId, schoolyear.id!);
    }

    public async Task update(string studentId, string enrollmentId)
    {
        FormattableString query = $"update academy.student set enrollmentid = {enrollmentId} where studentid = {studentId};";
        await context.Database.ExecuteSqlAsync(query);
    }
 
    public async Task<StudentEntity> getCurrentById(string studentId)
    {
        var schoolyear = await daoFactory.schoolyearDao!.getCurrentOrNew();
        
        var result = await getById(studentId, schoolyear.id!);
        if (result == null)
        {
            throw new EntityNotFoundException("AcademyStudent", studentId);
        }

        return result;
    }

    private async Task<StudentEntity?> getById(string studentId, string schoolyearId)
    {
        return await entities.FirstOrDefaultAsync(e => e.studentId == studentId && e.schoolYear == schoolyearId);
    }
}