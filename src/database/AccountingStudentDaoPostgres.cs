using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.database.service;
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

        if (result == null)
        {
            return "Sin matrícula";
        }

        result.initLabels();
        return result.enrollment!;
    }

    public async Task<PagedResult<StudentView>> getPaginatedStudentView(PagedRequest request)
    {
        var query = context.GetQueryable<StudentView>().Where(e => e.isActive);

        var pagedService = new PagedService<StudentView>(query, search);

        request.setDefaultSort("fullName");
        return await pagedService.getPaged(request);
    }

    private static IQueryable<StudentView> search(IQueryable<StudentView> query, string search)
    {
        var value = $"%{search}%";

        return query.Where(e =>
            EF.Functions.Like(e.studentId, value) ||
            EF.Functions.Like(e.fullName.ToLower(), value) ||
            EF.Functions.Like(e.tutor.ToLower(), value) ||
            (e.enrollment != null && EF.Functions.Like(e.enrollment.ToLower(), value)));
    }

    public async Task<List<StudentEntity>> getAllWithEnrollmentTariffSolvency()
    {
        var schoolyear = await daoFactory.schoolyearDao!.getNewOrCurrent();

        return await entities.AsNoTracking().Join
        (
            context.Set<StudentEnrollPaymentView>()
                .Where(e => e.schoolyearId == schoolyear.id && e.enrollmentId == null),
            e => e.studentId,
            view => view.studentId,
            (e, view) => e
        ).Include(e => e.student).ToListAsync();
    }

    public async Task<List<DebtorStudentView>> getDebtorStudentList()
    {
        return await context.Set<DebtorStudentView>().ToListAsync();
    }

    public async Task<bool> hasEnrollmentTariffSolvency(string studentId)
    {
        var schoolyear = await daoFactory.schoolyearDao!.getNewOrCurrent();

        var result = await context.Set<StudentEnrollPaymentView>()
            .Where(e => e.studentId == studentId && e.schoolyearId == schoolyear.id)
            .FirstOrDefaultAsync();

        return result != null;
    }
}