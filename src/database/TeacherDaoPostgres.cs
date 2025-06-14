using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.database;

public class TeacherDaoPostgres(PostgresContext context) : GenericDaoPostgres<TeacherEntity, string>(context), ITeacherDao
{
    public new async Task<TeacherEntity?> getById(string id)
    {
        return await entities
            .Include(e => e.user)
            .Include(e => e.enrollmentList)
            .FirstOrDefaultAsync(e => e.teacherId == id);
    }

    public new async Task<List<TeacherEntity>> getAll()
    {
        var result = await entities.Include(e => e.user).ToListAsync();
        if (result.Count == 0)
        {
            throw new ConflictException("There is not teacher in the records.");
        }

        return result;
    }

    public async Task<TeacherEntity?> getByEnrollmentId(string enrollmentId)
    {
        var enrollment = await context.Set<EnrollmentEntity>().AsNoTracking()
            .FirstOrDefaultAsync(e => e.enrollmentId == enrollmentId);
        
        if (enrollment == null)
        {
            throw new EntityNotFoundException("EnrollmentEntity", enrollmentId);
        }
        
        if (enrollment.teacherId == null)
        {
            throw new EntityNotFoundException("TeacherEntity", "null");
        }

        return await getById(enrollment.teacherId);
    }

    public async Task<TeacherEntity> getByUserId(Guid userId)
    {
        var result = await entities.FirstOrDefaultAsync(e => e.userId == userId);
        if (result == null)
        {
            throw new EntityNotFoundException($"Teacher wit user id ({userId.ToString()}) not found.");
        }

        return result;
    }


    public async Task<List<TeacherEntity>> getListWithSubjectGradedForCurrentPartial()
    {
        var daoFactory = new DaoFactoryPostgres(context);
        var partialList = await daoFactory.partialDao.getListForCurrentSchoolyear();
        
        var currentPartial = partialList.FirstOrDefault(e => e.recordIsActive());
        if (currentPartial == null)
        {
            return [];
        }
        
        return await entities.AsNoTracking().GroupJoin(context.Set<SubjectGradedView>(),
            e => e.teacherId,
            s => s.teacherId,
            (item, subjectGradedList) => new TeacherEntity
            {
                userId = item.userId,
                teacherId = item.teacherId,
                isGuide = item.isGuide,
                user = item.user,
                subjectGradedList = subjectGradedList.Where(e => e.partialId == currentPartial.partialId).ToList()
            }).ToListAsync();
    }

    public async Task<string> getCurrentEnrollmentId(string teacherId)
    {
        var teacher = await getById(teacherId);
        if (teacher == null)
        {
            throw new EntityNotFoundException("TeacherEntity", teacherId);
        }

        var daoFactory = new DaoFactoryPostgres(context);
        await teacher.setCurrentEnrollment(daoFactory.schoolyearDao);
        if (!teacher.hasCurrentEnrollment())
        {
            throw new EntityNotFoundException($"Entity of type (EnrollmentEntity) with teacher id ({teacherId}) for current schoolyear not found.");
        }

        return teacher.getCurrentEnrollmentId();
    }
}