using Microsoft.EntityFrameworkCore;
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

    public async Task<StudentEntity> getFullById(string studentId)
    {
        var student = await entities.Where(e => e.studentId == studentId)
            .Include(e => e.discount)
            .Include(e => e.student)
            .ThenInclude(d => d.tutor)
            .Include(e => e.debtHistory)!
            .ThenInclude(e => e.tariff)
            .Include(e => e.transactions)!
            .ThenInclude(t => t.details)
            .ThenInclude(a => a.tariff)
            .FirstOrDefaultAsync();

        if (student == null)
        {
            throw new EntityNotFoundException("StudentEntity", studentId);
        }

        student.enrollmentLabel = await getEnrollmentLabel(studentId);
        return student;
    }
    
    private async Task<string> getEnrollmentLabel(string studentId)
    {
        var result = await context.Set<StudentView>().AsNoTracking()
            .Where(e => e.studentId == studentId).FirstOrDefaultAsync();
        
        if(result == null) return "Sin matrícula";
        
        result.initLabels();
        return result.enrollment!;
    }
    

    public async Task<List<StudentView>> getStudentViewList()
    {
        return await context.Set<StudentView>().AsNoTracking()
            .Where(e => e.isActive).ToListAsync();
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
        query += $" WHERE s.studentstate = true AND ({tariffsId}) AND aca.enrollmentid is NULL AND";
        query += " CASE";
        query += "   WHEN d.amount = 0 THEN 1";
        query += "   ELSE (d.debtbalance / d.amount)";
        query += " END >= 0.4";

        return await entities.FromSqlRaw(query)
            .Include(e => e.student).AsNoTracking()
            .ToListAsync();
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
        query += $" WHERE s.studentstate = true AND s.studentid = '{studentId}' AND ({tariffsId}) AND";
        query += " CASE";
        query += "   WHEN d.amount = 0 THEN 1";
        query += "   ELSE (d.debtbalance / d.amount)";
        query += " END >= 0.4";

        return await entities.FromSqlRaw(query).AsNoTracking().FirstOrDefaultAsync() != null;
    }
}