using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
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

    public async Task<bool> isEnrolled(string studentId)
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
        FormattableString query =
            $"update academy.student set enrollmentid = {enrollmentId} where studentid = {studentId};";
        await context.Database.ExecuteSqlAsync(query);
    }

    public async Task<StudentEntity> getCurrentById(string studentId)
    {
        var schoolyear = await daoFactory.schoolyearDao!.getCurrentOrNew();

        var result = await getById(studentId, schoolyear.id!);
        if (result == null)
        {
            throw new EntityNotFoundException("Academy.StudentEntity", studentId);
        }

        return result;
    }

    public async Task<List<StudentEntity>> getListWithGradesForCurrentSchoolyear(string enrollmentId, int partial)
    {
        return await entities
            .GroupJoin(
                context.Set<GradeView>().Where(e => e.enrollmentId == enrollmentId && e.partial == partial),
                s => s.studentId,
                g => g.studentId, 
                (std, gradeList) => new {std, gradeList})
            .GroupJoin(
                context.Set<GradeAverageView>().Where(e => e.enrollmentId == enrollmentId && e.partial == partial),
                temp => temp.std.studentId,
                g => g.studentId,
                (temp, average) => new StudentEntity
                {
                    studentId = temp.std.studentId,
                    enrollmentId = temp.std.enrollmentId,
                    schoolyearId = temp.std.schoolyearId,
                    isApproved = temp.std.isApproved,
                    isRepeating = temp.std.isRepeating,
                    createdAt = temp.std.createdAt,
                    gradeList = temp.gradeList.ToList(),
                    averageList = average.ToList()
                }).Include(e => e.student)
            .ToListAsync();
    }

    private async Task<StudentEntity?> getById(string studentId, string schoolyearId)
    {
        return await entities.Include(e => e.student)
            .FirstOrDefaultAsync(e => e.studentId == studentId && e.schoolyearId == schoolyearId);
    }
}