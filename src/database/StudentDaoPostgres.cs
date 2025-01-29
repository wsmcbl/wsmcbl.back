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
        var entity = await entities.Include(e => e.tutor)
            .Include(e => e.file)
            .Include(e => e.parents)
            .Include(e => e.measurements)
            .FirstOrDefaultAsync(e => e.studentId == id);
        
        if (entity == null)
        {
            throw new EntityNotFoundException("StudentEntity", id);
        }

        return entity;
    }

    public async Task<StudentEntity?> findDuplicateOrNull(StudentEntity student)
    {
        var list = await context.Set<StudentEntity>().Where(e => student.name == e.name).ToListAsync();
        return list.Find(e => student.getStringData().Equals(e.getStringData()));
    }

    public async Task<List<StudentView>> getStudentViewList()
    {
        return await context.Set<StudentView>().ToListAsync();
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