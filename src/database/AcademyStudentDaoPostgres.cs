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

    private async Task<StudentEntity?> getById(string studentId, string schoolyearId)
    {
        return await entities.Include(e => e.student)
            .FirstOrDefaultAsync(e => e.studentId == studentId && e.schoolyearId == schoolyearId);
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

    public async Task<StudentEntity> getCurrentWithGradeById(string studentId)
    {
        var schoolyear = await daoFactory.schoolyearDao!.getNewOrCurrent();
        return await getByIdWithGrade(studentId, schoolyear.id!);
    }

    public async Task<StudentEntity> getByIdWithGrade(string studentId, string schoolyearId)
    {
        var student = await getById(studentId, schoolyearId) ?? await getWithdrawnStudentById(studentId, schoolyearId);

        student.gradeList = await context.Set<GradeView>()
            .Where(g => g.studentId == student.studentId && g.schoolyearId == schoolyearId)
            .ToListAsync();

        student.averageList = await context.Set<GradeAverageView>()
            .Where(g => g.studentId == student.studentId && g.schoolyearId == schoolyearId)
            .ToListAsync();
        
        return student;
    }

    private async Task<StudentEntity> getWithdrawnStudentById(string studentId, string schoolyearId)
    {
        var result = await daoFactory.withdrawnStudentDao!.getBySchoolyearId(studentId, schoolyearId);
        
        return new StudentEntity(studentId, result.lastEnrollmentId)
        {
            schoolyearId = result.schoolyearId,
            student = (await daoFactory.studentDao!.getById(studentId))!
        };
    }

    public async Task<List<StudentEntity>> getListWithGradesByEnrollmentId(string enrollmentId, int partialId)
    {
        var studentList = await entities.AsNoTracking()
            .Where(e => e.enrollmentId == enrollmentId)
            .Include(e => e.student)
            .ToListAsync();

        var studentIdSet = new HashSet<string>(studentList.Select(s => s.studentId).ToList());

        var gradeList = await context.Set<GradeView>()
            .Where(g => g.partialId == partialId && studentIdSet.Contains(g.studentId))
            .ToListAsync();

        var averageList = await context.Set<GradeAverageView>()
            .Where(g => g.partialId == partialId && studentIdSet.Contains(g.studentId))
            .ToListAsync();

        return studentList.Select(std => new StudentEntity
            {
                studentId = std.studentId,
                enrollmentId = std.enrollmentId,
                schoolyearId = std.schoolyearId,
                isApproved = std.isApproved,
                isRepeating = std.isRepeating,
                createdAt = std.createdAt,
                student = std.student,
                gradeList = gradeList.Where(g => g.studentId == std.studentId).ToList(),
                averageList = averageList.Where(g => g.studentId == std.studentId).ToList()
            })
            .OrderBy(e => e.student.sex)
            .ThenBy(e => e.student.name)
            .ToList();
    }

    public async Task<List<StudentEntity>> getListWithGradesByDegreeId(string degreeId, int partialId)
    {
        List<StudentEntity> result = [];
        
        var degree = await daoFactory.degreeDao!.getById(degreeId);
        
        foreach (var enrollment in degree!.enrollmentList!)
        {
            result.AddRange(await getListWithGradesByEnrollmentId(enrollment.enrollmentId!, partialId));
        }

        return result;
    }

    public async Task<List<StudentEntity>> getListBeforeFirstPartialByDegreeId(string degreeId)
    {
        var partialList = await daoFactory.partialDao!.getListForCurrentSchoolyear();
        
        var firstPartial = partialList.FirstOrDefault(e => e is { semester: 1, partial: 1 });
        if (firstPartial == null)
        {
            throw new ConflictException("There is not initial partial.");
        }
        
        return await entities.AsNoTracking()
            .Where(std => context.Set<EnrollmentEntity>().Any(e => e.enrollmentId == std.enrollmentId && e.degreeId == degreeId))
            .Where(e => firstPartial.startDate >= DateOnly.FromDateTime(e.createdAt))
            .Include(e => e.student).ToListAsync();
    }

    public async Task<List<StudentEntity>> getListWithAverageGradesByEnrollmentId(string enrollmentId)
    {
        var studentList = await entities.AsNoTracking()
            .Where(e => e.enrollmentId == enrollmentId)
            .Include(e => e.student)
            .ToListAsync();

        var studentIdSet = new HashSet<string>(studentList.Select(s => s.studentId).ToList());
        var schoolyear = await daoFactory.schoolyearDao!.getCurrent();

        var averageList = await context.Set<GradeAverageView>()
            .Where(g => studentIdSet.Contains(g.studentId) && g.schoolyearId == schoolyear.id)
            .ToListAsync();

        return studentList.Select(std => new StudentEntity
            {
                studentId = std.studentId,
                enrollmentId = std.enrollmentId,
                schoolyearId = std.schoolyearId,
                isApproved = std.isApproved,
                isRepeating = std.isRepeating,
                createdAt = std.createdAt,
                student = std.student,
                gradeList = [],
                averageList = averageList.Where(g => g.studentId == std.studentId).ToList()
            })
            .OrderBy(e => e.student.sex)
            .ThenBy(e => e.student.name)
            .ToList();
    }

    public async Task<List<StudentEntity>> getListBeforeFirstPartial(string? enrollmentId = null)
    {
        var partialList = await daoFactory.partialDao!.getListForCurrentSchoolyear();
        
        var firstPartial = partialList.FirstOrDefault(e => e is { semester: 1, partial: 1 });
        if (firstPartial == null)
        {
            throw new ConflictException("There is not initial partial.");
        }

        if (enrollmentId == null)
        {
            return await entities
                .Where(e => firstPartial.startDate >= DateOnly.FromDateTime(e.createdAt))
                .Include(e => e.student)
                .ToListAsync();
        }

        return await entities.Where(e => e.enrollmentId == enrollmentId)
            .Where(e => firstPartial.startDate >= DateOnly.FromDateTime(e.createdAt))
            .Include(e => e.student)
            .ToListAsync();
    }
    
    public async Task update(string studentId, string enrollmentId)
    {
        FormattableString query =
            $"update academy.student set enrollmentid = {enrollmentId} where studentid = {studentId};";
        await context.Database.ExecuteSqlAsync(query);
    }
}