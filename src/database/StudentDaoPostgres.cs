using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.secretary;
using IStudentDao = wsmcbl.src.model.secretary.IStudentDao;
using StudentEntity = wsmcbl.src.model.secretary.StudentEntity;

namespace wsmcbl.src.database;

public class StudentDaoPostgres(PostgresContext context)
    : GenericDaoPostgres<StudentEntity, string>(context), IStudentDao
{
    public async Task<StudentEntity> getByIdWithProperties(string id)
    {
        var entity = await entities.FirstOrDefaultAsync(e => e.studentId == id);

        if (entity == null)
        {
            throw new EntityNotFoundException("Student", id);
        }

        var tutor = await context.Set<StudentTutorEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.tutorId == entity.tutorId);

        if (tutor == null)
        {
            throw new EntityNotFoundException($"Entity of type (Tutor) with StudentId ({id}) not found.");
        }

        entity.tutor = tutor;

        entity.measurements = await context.Set<StudentMeasurementsEntity>().AsNoTracking()
            .FirstOrDefaultAsync(e => e.studentId == id);

        entity.file = await context.Set<StudentFileEntity>().AsNoTracking()
            .FirstOrDefaultAsync(e => e.studentId == id);

        entity.parents = await context.Set<StudentParentEntity>()
            .Where(e => e.studentId == id)
            .AsNoTracking()
            .ToListAsync();

        return entity;
    }

    public async Task<StudentEntity?> getByInformation(StudentEntity student)
    {
        return (await context.Set<StudentEntity>().Where(e => student.name == e.name).ToListAsync())
            .Find(e => student.getStringData().Equals(e.getStringData()));
    }

    public async Task<List<(StudentEntity student, string schoolyear, string enrollment)>> getListWhitSchoolyearAndEnrollment()
    {
        var studentList = await getAll();
        var academyList = await context.Set<model.academy.StudentEntity>().ToListAsync();
        var result = new List<(StudentEntity student, string schoolyear, string enrollment)>();
        foreach (var item in studentList)
        {
            var academyStudent = academyList.Where(e => e.studentId == item.studentId)
                .MaxBy(e => GetNumericPart(e.schoolYear));

            var schoolyearLabel = "";
            var enrollmentLabel = "Sin matrícula";
            if (academyStudent == null)
            {
                result.Add((item, schoolyearLabel, enrollmentLabel));
                continue;
            }

            var schoolyear = await context.Set<SchoolYearEntity>()
                .FirstOrDefaultAsync(e => e.id == academyStudent.schoolYear);
            if (schoolyear != null)
                schoolyearLabel = schoolyear.label;

            var enrollment = await context.Set<EnrollmentEntity>()
                .FirstOrDefaultAsync(e => e.enrollmentId == academyStudent.enrollmentId);
            if (enrollment != null)
                enrollmentLabel = enrollment.label;
            
            result.Add((item, schoolyearLabel, enrollmentLabel));
        }

        return result;
    }
    

    private static int GetNumericPart(string item)
    {
        var numericPart = item.Substring(3);
        return int.Parse(numericPart);
    }

    public async Task<List<StudentEntity>> getAllWithSolvency()
    {
        var schoolyearDao = new SchoolyearDaoPostgres(context);
        var ID = await schoolyearDao.getCurrentAndNewSchoolyearIds();

        var tariffList = await context.Set<model.accounting.TariffEntity>()
            .Where(e => e.schoolYear == ID.currentSchoolyear || e.schoolYear == ID.newSchoolyear)
            .Where(e => e.type == Const.TARIFF_REGISTRATION).ToListAsync();

        if (tariffList.Count == 0)
        {
            throw new EntityNotFoundException(
                $"Entities of type (Tariff) with type ({Const.TARIFF_REGISTRATION}) not found.");
        }

        var tariffsId = string.Join(" OR ", tariffList.Select(item => $"d.tariffid = {item.tariffId}"));
        var query = $@"SELECT s.* FROM secretary.student s
                    INNER JOIN accounting.debthistory d ON d.studentid = s.studentid
                    LEFT JOIN academy.student aca on aca.studentid = s.studentid
                    WHERE ({tariffsId}) AND (d.debtbalance / d.amount) > 0.45 AND aca.enrollmentid is NULL;";
        
        return await entities.FromSqlRaw(query).AsNoTracking().ToListAsync();
    }

    public async Task updateAsync(StudentEntity? entity)
    {
        if (entity == null)
        {
            return;
        }

        var existingStudent = await getById(entity.studentId!);
        if (existingStudent == null)
        {
            throw new EntityNotFoundException("StudentEntity", entity.studentId);
        }

        existingStudent.update(entity);
        update(existingStudent);
    }
}