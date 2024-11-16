using Microsoft.EntityFrameworkCore;
using Npgsql;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.database;

public class AccountingStudentDaoPostgres(PostgresContext context) : GenericDaoPostgres<StudentEntity, string>(context), IStudentDao
{
    public new async Task<List<StudentEntity>> getAll()
    {
        FormattableString query = $@"select std.* from accounting.student std 
        join secretary.student as sec_std on sec_std.studentid = std.studentid
        where sec_std.studentstate = 'true'";

        var list = await context.Set<StudentEntity>()
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

    public async Task<StudentEntity> getWithoutPropertiesById(string studentId)
    {
        return (await entities.FirstOrDefaultAsync(e => e.studentId == studentId))!;
    }

    public new async Task<StudentEntity?> getById(string id)
    {
        var student = await entities
            .Include(e => e.discount)
            .Include(e => e.student)
            .ThenInclude(d => d.tutor)
            .Include(e => e.transactions)!
            .ThenInclude(t => t.details)
            .FirstOrDefaultAsync(e => e.studentId == id);
        
        if (student is null)
        {
            throw new EntityNotFoundException("Student", id);
        }

        await setEnrollmentLabel(student);
        
        foreach (var transaction in student.transactions!)
        {
            foreach (var item in transaction.details)
            {
                var tariff = await context.Set<TariffEntity>().FirstOrDefaultAsync(t => t.tariffId == item.tariffId);
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
            student.enrollmentLabel = await reader.ReadAsync() ? reader.GetString(0) : "Sin matrícula";
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