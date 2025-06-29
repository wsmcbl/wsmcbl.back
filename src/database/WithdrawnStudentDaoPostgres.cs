using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.database;

public class WithdrawnStudentDaoPostgres : GenericDaoPostgres<WithdrawnStudentEntity, int>, IWithdrawnStudentDao
{
    private DaoFactory daoFactory { get; set; }
    
    public WithdrawnStudentDaoPostgres(PostgresContext context) : base(context)
    {
        daoFactory = new DaoFactoryPostgres(context);
    }
    
    public async Task<WithdrawnStudentEntity> getBySchoolyearId(string studentId, string schoolyearId)
    {
        var result = await entities
            .FirstOrDefaultAsync(e => e.studentId == studentId && e.schoolyearId == schoolyearId);
        
        if (result == null)
        {
            throw new EntityNotFoundException("WithdrawnStudentEntity", $"studentId: {studentId}, schoolyearId: {schoolyearId}");
        }

        return result;
    }
    
    public new async Task<List<WithdrawnStudentEntity>> getAll()
    {
        return await entities.AsNoTracking()
            .Include(e => e.student)
            .Include(e => e.lastEnrollment)
            .ToListAsync();
    }

    public async Task<List<WithdrawnStudentEntity>> getAllForCurrentSchoolyear()
    {
        var currentSchoolyear = await daoFactory.schoolyearDao!.getCurrent();
        return await entities.AsNoTracking()
            .Where(e => e.schoolyearId == currentSchoolyear.id).ToListAsync();
    }

    public async Task<List<WithdrawnStudentEntity>> getListByDegreeId(string degreeId)
    {
        var partialList = await daoFactory.partialDao!.getListForCurrentSchoolyear();

        var firstPartial = partialList.FirstOrDefault(e => e is { semester: 1, partial: 1 });
        if (firstPartial == null)
        {
            throw new ConflictException("There is not initial partial.");
        }
        
        return await entities.AsNoTracking()
            .Where(std => context.Set<EnrollmentEntity>().Any(e => e.enrollmentId == std.lastEnrollmentId && e.degreeId == degreeId))
            .Where(e => firstPartial.startDate >= DateOnly.FromDateTime(e.enrolledAt))
            .Include(e => e.student).ToListAsync();
    }

    public async Task<List<WithdrawnStudentEntity>> getListByEnrollmentId(string enrollmentId, bool hasBeforeFirstPartial = false)
    {
        var query = entities.Where(e => e.lastEnrollmentId == enrollmentId);

        if (!hasBeforeFirstPartial)
        {
            return await query.Include(e => e.student).ToListAsync();
        }
        
        var partialList = await daoFactory.partialDao!.getListForCurrentSchoolyear();

        var firstPartial = partialList.FirstOrDefault(e => e is { semester: 1, partial: 1 });
        if (firstPartial == null)
        {
            throw new ConflictException("There is not initial partial.");
        }

        return await query.Where(e => firstPartial.startDate >= DateOnly.FromDateTime(e.enrolledAt))
            .Include(e => e.student).ToListAsync();
    }
}