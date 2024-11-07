using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.database;

public class EnrollmentDaoPostgres(PostgresContext context) : GenericDaoPostgres<EnrollmentEntity, string>(context), IEnrollmentDao
{
    public new async Task<List<EnrollmentEntity>> getAll()
    {
        return await entities
            .Include(e => e.studentList)
            .Include(e => e.subjectList)
            .ToListAsync();
    }

    public async Task<EnrollmentEntity> getFullById(string enrollmentId)
    {
        var result = await entities
            .Include(e => e.studentList)!
            .ThenInclude(e => e.student)
            .Include(e => e.subjectList)!
            .ThenInclude(e => e.secretarySubject)
            .FirstOrDefaultAsync(e => e.enrollmentId == enrollmentId);

        if (result == null)
        {
            throw new EntityNotFoundException("Enrollment", enrollmentId);
        }

        return result;
    }

    public async Task<EnrollmentEntity> getByStudentId(string? studentId)
    {
        var student = await context.Set<StudentEntity>().FirstOrDefaultAsync(e => e.studentId == studentId);

        if (student == null)
        {
            throw new EntityNotFoundException($"There is not Enrollment that contains the student with id ({studentId}).");
        }

        var result = await entities
            .FirstOrDefaultAsync(e => e.enrollmentId == student.enrollmentId);

        if (result == null)
        {   
            throw new EntityNotFoundException("Enrollment", student.enrollmentId);
        }

        return result;
    }

    public async Task<List<EnrollmentEntity>> getListByTeacherId(string teacherId)
    {
        var subjectList = await context.Set<SubjectEntity>()
            .Where(e => e.teacherId == teacherId).ToListAsync();

        var schoolyearDao = new SchoolyearDaoPostgres(context);
        var currentSchoolyear = await schoolyearDao.getCurrentSchoolyear();

        var enrollmentList = await entities.Where(e => e.schoolYear == currentSchoolyear.id).ToListAsync();

        List<EnrollmentEntity> result = [];
        foreach (var item in subjectList)
        {
            result.Add(enrollmentList.Find(e => e.enrollmentId == item.enrollmentId)!);
        }
        
        return result;
    }
}