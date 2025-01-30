using Microsoft.EntityFrameworkCore;
using Npgsql;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.database;

public class AccountingStudentDaoPostgres : GenericDaoPostgres<StudentEntity, string>, IStudentDao
{
    private DaoFactory daoFactory { get; set; }

    public AccountingStudentDaoPostgres(PostgresContext context) : base(context)
    {
        daoFactory = new DaoFactoryPostgres(context);
    }
    
    public new async Task<List<StudentEntity>> getAll()
    {
        FormattableString query = $@"select acc.* from accounting.student acc 
        join secretary.student as s on s.studentid = acc.studentid
        where s.studentstate = 'true'";

        var list = await entities
            .FromSqlInterpolated(query)
            .AsNoTracking()
            .Include(e => e.student)
            .ThenInclude(e => e.tutor)
            .ToListAsync();
        
        if(list.Count != 0)
        {
            await setEnrollmentLabelsForStudents(list);
        }

        return list;
    }

    public async Task<bool> hasSolvencyInRegistration(string studentId)
    {
        var tariffList = await daoFactory.tariffDao!.getCurrentRegistrationTariffList();
        if (tariffList.Count == 0)
        {
            throw new EntityNotFoundException(
                $"Entities of type (TariffEntity) with type ({Const.TARIFF_REGISTRATION}) not found.");
        }

        var tariffsId = string.Join(" OR ", tariffList.Select(item => $"d.tariffid = {item.tariffId}"));
        var query = "SELECT s.* FROM accounting.student s";
        query += " INNER JOIN accounting.debthistory d ON d.studentid = s.studentid";
        query += $" WHERE studentid = '{studentId}' AND ({tariffsId}) AND";
        query += " CASE";
        query += "   WHEN d.amount = 0 THEN 1";
        query += "   ELSE (d.debtbalance / d.amount)";                              
        query += " END >= 0.4;";
        
        return await entities.FromSqlRaw(query).AsNoTracking().FirstOrDefaultAsync() != null;
    }

    public async Task<List<StudentEntity>> getAllWithSolvencyInRegistration()
    {
        var tariffList = await daoFactory.tariffDao!.getCurrentRegistrationTariffList();
        if (tariffList.Count == 0)
        {
            throw new EntityNotFoundException(
                $"Entities of type (Tariff) with type ({Const.TARIFF_REGISTRATION}) not found.");
        }

        var tariffsId = string.Join(" OR ", tariffList.Select(item => $"d.tariffid = {item.tariffId}"));
        var query = "SELECT s.* FROM accounting.student s";
        query += " INNER JOIN accounting.debthistory d ON d.studentid = s.studentid";
        query += " LEFT JOIN academy.student aca on aca.studentid = s.studentid";
        query += $" WHERE ({tariffsId}) AND aca.enrollmentid is NULL AND";
        query += " CASE";
        query += "   WHEN d.amount = 0 THEN 1";
        query += "   ELSE (d.debtbalance / d.amount)";                              
        query += " END >= 0.4;";
        
        return await entities.FromSqlRaw(query).AsNoTracking().ToListAsync();
    }

    public async Task<StudentEntity> getFullById(string studentId)
    {
        var student = await entities.Where(e => e.studentId == studentId)
            .Include(e => e.discount)
            .Include(e => e.student)
            .ThenInclude(d => d.tutor)
            .Include(e => e.transactions)!
            .ThenInclude(t => t.details)
            .Include(e => e.debtHistory)!
            .ThenInclude(e => e.tariff)
            .FirstOrDefaultAsync();
        
        if (student == null)
        {
            throw new EntityNotFoundException("StudentEntity", studentId);
        }

        await setEnrollmentLabel(student);
        
        foreach (var transaction in student.transactions!)
        {
            foreach (var item in transaction.details)
            {
                var tariff = await daoFactory.tariffDao!.getById(item.tariffId);
                item.setTariff(tariff);
            }
        }
        
        return student;
    }
    
    private async Task setEnrollmentLabel(StudentEntity student)
    {
        const string sql = @"SELECT enroll.label
                       FROM academy.student AS std
                       JOIN academy.enrollment AS enroll ON enroll.enrollmentid = std.enrollmentid
                       WHERE std.studentid = @p0
                       LIMIT 1";

        await using var command = context.Database.GetDbConnection().CreateCommand();
        command.CommandText = sql;
        command.Parameters.Add(new NpgsqlParameter("@p0", student.studentId));
        await context.Database.OpenConnectionAsync();

        await using (var reader = await command.ExecuteReaderAsync())
        {
            student.setEnrollmentLabel(await reader.ReadAsync() ? reader.GetString(0) : null);
        }
        
        await context.Database.CloseConnectionAsync();
    }
    
    private async Task setEnrollmentLabelsForStudents(List<StudentEntity> students)
    {
        var studentIds = students.Select(s => s.studentId).ToList();
        var studentIdsInSql = string.Join(",", studentIds.Select(id => $"'{id}'"));

        var sql = $@"
        SELECT std.studentid, enroll.label
        FROM academy.student AS std
        JOIN academy.enrollment AS enroll ON enroll.enrollmentid = std.enrollmentid
        WHERE std.studentid IN ({studentIdsInSql})";

        await context.Database.OpenConnectionAsync();
        await using (var command = context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = sql;
            await using (var reader = await command.ExecuteReaderAsync())
            {
                var enrollmentLabels = new Dictionary<string, string>();
                while (await reader.ReadAsync())
                {
                    var studentId = reader.GetString(0);
                    var enrollmentLabel = reader.GetString(1);

                    enrollmentLabels[studentId] = enrollmentLabel;
                }

                foreach (var student in students)
                {
                    student.enrollmentLabel = enrollmentLabels
                        .GetValueOrDefault(student.studentId!, "Sin matrícula");
                }
            }
        }

        await context.Database.CloseConnectionAsync();
    }
}