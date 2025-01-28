using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.database;

public class StudentDaoPostgres : GenericDaoPostgres<StudentEntity, string>, IStudentDao
{
    private DaoFactory daoFactory { get; set; }
    public StudentDaoPostgres(PostgresContext context) : base(context)
    {
        daoFactory = new DaoFactoryPostgres(context);
    }

    public async Task<StudentEntity> getFullById(string id)
    {
        var entity = await entities.FirstOrDefaultAsync(e => e.studentId == id);
        if (entity == null)
        {
            throw new EntityNotFoundException("StudentEntity", id);
        }

        var tutor = await context.Set<StudentTutorEntity>().AsNoTracking()
            .FirstOrDefaultAsync(e => e.tutorId == entity.tutorId);
        if (tutor == null)
        {
            throw new EntityNotFoundException($"Entity of type (TutorEntity) with StudentId ({id}) not found.");
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

    public async Task<StudentEntity?> findByDuplicateOrNull(StudentEntity student)
    {
        var list = await context.Set<StudentEntity>().Where(e => student.name == e.name).ToListAsync();
        return list.Find(e => student.getStringData().Equals(e.getStringData()));
    }

    public async Task<List<(StudentEntity student, string schoolyear, string enrollment)>> getListWhitSchoolyearAndEnrollment()
    {
        var studentList = await getAll();
        var academyList = await daoFactory.academyStudentDao!.getAll();
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

            var schoolyear = await daoFactory.schoolyearDao!.getById(academyStudent.schoolYear);
            if (schoolyear != null)
            {
                schoolyearLabel = schoolyear.label;
            }

            var enrollment = await daoFactory.enrollmentDao!.getById(academyStudent.enrollmentId ?? string.Empty);
            if (enrollment != null)
            {
                enrollmentLabel = enrollment.label;
            }
            
            result.Add((item, schoolyearLabel, enrollmentLabel));
        }

        return result;
    }
    

    private static int GetNumericPart(string item)
    {
        var numericPart = item.Substring(3);
        return int.Parse(numericPart);
    }

    public async Task<List<StudentEntity>> getAllWithSolvencyInRegistration()
    {
        var schoolyear = await daoFactory.schoolyearDao!.getNewOrCurrent();

        var tariffList = await context.Set<model.accounting.TariffEntity>()
            .Where(e => e.schoolYear == schoolyear.id)
            .Where(e => e.type == Const.TARIFF_REGISTRATION).ToListAsync();

        if (tariffList.Count == 0)
        {
            throw new EntityNotFoundException(
                $"Entities of type (Tariff) with type ({Const.TARIFF_REGISTRATION}) not found.");
        }

        var tariffsId = string.Join(" OR ", tariffList.Select(item => $"d.tariffid = {item.tariffId}"));
        var query = "SELECT s.* FROM secretary.student s";
        query += " INNER JOIN accounting.debthistory d ON d.studentid = s.studentid";
        query += " LEFT JOIN academy.student aca on aca.studentid = s.studentid";
        query += $" WHERE ({tariffsId}) AND aca.enrollmentid is NULL AND";
        query += " CASE";
        query += "   WHEN d.amount = 0 THEN 1";
        query += "   ELSE (d.debtbalance / d.amount)";                              
        query += " END >= 0.4;";
        
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

    public new async Task delete(StudentEntity entity)
    {
        FormattableString query =$"delete from secretary.schoolyear_student where studentid = {entity.studentId};";
        await context.Database.ExecuteSqlAsync(query);
        
        await base.delete(entity);
    }
}