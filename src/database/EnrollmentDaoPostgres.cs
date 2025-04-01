using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.database;

public class EnrollmentDaoPostgres : GenericDaoPostgres<EnrollmentEntity, string>, IEnrollmentDao
{
    private DaoFactory daoFactory {get; set;}

    public EnrollmentDaoPostgres(PostgresContext context) : base(context)
    {
        daoFactory = new DaoFactoryPostgres(context);
    }
    
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
            .Where(e => e.enrollmentId == enrollmentId)
            .Include(e => e.studentList)!
            .ThenInclude(e => e.student)
            .Include(e => e.subjectList)!
            .ThenInclude(e => e.secretarySubject)
            .FirstOrDefaultAsync();

        if (result == null)
        {
            throw new EntityNotFoundException("EnrollmentEntity", enrollmentId);
        }

        return result;
    }

    public async Task<EnrollmentEntity> getByStudentId(string studentId)
    {
        var student = await daoFactory.academyStudentDao!.getById(studentId);
        if (student == null)
        {
            throw new EntityNotFoundException($"There is not Enrollment that contains the student with id ({studentId}).");
        }

        var result = await getById(student.enrollmentId ?? string.Empty);
        if (result == null)
        {   
            throw new EntityNotFoundException("EnrollmentEntity", student.enrollmentId);
        }

        return result;
    }

    public async Task<List<EnrollmentEntity>> getListByTeacherId(string teacherId)
    {
        var currentSchoolyear = await daoFactory.schoolyearDao!.getCurrent();

        var enrollmentList = await entities.Where(e => e.schoolYear == currentSchoolyear.id)
            .Include(e => e.subjectList)
            .ToListAsync();

        var subjectList = await daoFactory.academySubjectDao!.getListByTeacherId(teacherId);
        
        var result = new List<EnrollmentEntity>();
        foreach (var item in subjectList)
        {
            var value = enrollmentList.FirstOrDefault(e => e.enrollmentId == item.enrollmentId);
            if (value != null)
            {
                result.Add(value);
            }
        }
        
        return result.Distinct().ToList();
    }

    public async Task createRange(ICollection<EnrollmentEntity> enrollmentList)
    {
        entities.AddRange(enrollmentList);
        await saveAsync();
    }

    public async Task<EnrollmentEntity> getByTeacherIdForCurrentSchoolyear(string teacherId, bool isFull = false)
    {
        var enrollmentId = await daoFactory.teacherDao!.getCurrentEnrollmentId(teacherId);
        
        var result = isFull ? await getFullById(enrollmentId) : await getById(enrollmentId);
        if (result == null)
        {
            throw new EntityNotFoundException($"Entity of type (EnrollmentEntity) with teacher id ({teacherId}) not found.");
        }

        return result;
    }
}