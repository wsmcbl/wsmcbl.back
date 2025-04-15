using wsmcbl.src.controller.service.sheet;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;
using SubjectEntity = wsmcbl.src.model.secretary.SubjectEntity;

namespace wsmcbl.src.controller.business;

public class ViewPrincipalDashboardController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<List<StudentRegisterView>> getStudentRegisterViewListForCurrentSchoolyear()
    {
        return await daoFactory.studentDao!.getStudentRegisterListForCurrentSchoolyear();
    }

    public async Task<List<DegreeEntity>> getDegreeListForCurrentSchoolyear()
    {
        var schoolyear = await daoFactory.schoolyearDao!.getCurrentOrNew();
        var degreeList = await daoFactory.degreeDao!.getListForSchoolyearId(schoolyear.id!);
        
        return degreeList.OrderBy(e => e.educationalLevel).ThenBy(e => e.tag).ToList();
    }

    public async Task<List<TeacherEntity>> getSummaryTeacherGrades()
    {
        return await daoFactory.teacherDao!.getListWithSubjectGradedForCurrentPartial();
    }
    
    public async Task<(decimal expectedIncomeThisMonth, decimal expectedIncomeReceived, decimal totalIncomeThisMonth)> getSummaryRevenue()
    {
        var controller = new CalculateMonthlyRevenueController(daoFactory);
        
        var date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        var totalList = await controller.getTotalReceived(date);
        
        var expected = await controller.getExpectedMonthly(date);
        var received = await controller.getExpectedMonthly(date, true);

        return (expected.Sum(e => e.amount),
            received.Sum(e => e.amount),
            totalList.Sum(e => e.amount));
    }

    public async Task<List<object>> getLastIncidents()
    {
        await Task.CompletedTask;
        return
        [
            new { id = "inc100", studentId = "2025-0001-aswc", description = "Mal comportamiento", type = 1 },
            new { id = "inc101", studentId = "2025-0061-qrdc", description = "Mal comportamiento", type = 2 }
        ];
    }

    public async Task<List<SubjectEntity>> getSubjectList()
    {
        return await daoFactory.subjectDao!.getAll();
    }

    public async Task<List<DegreeEntity>> getDegreeList()
    {
        var currentSchoolyear = await daoFactory.schoolyearDao!.getCurrent();
        return await daoFactory.degreeDao!.getListForSchoolyearId(currentSchoolyear.id!);
    }

    public async Task<byte[]> getGradeSummaryByEnrollmentId(string enrollmentId, int partial, string userId)
    {
        var sheetMaker = new SpreadSheetMaker(daoFactory);
        return await sheetMaker.getEnrollmentGradeSummary(enrollmentId, partial, userId);
    }

    public async Task<List<EnrollmentEntity>> getEnrollmentList()
    {
        var result = await daoFactory.enrollmentDao!.getAllForCurrentSchoolyear();
        return result.Where(e => e.quantity != 0 || e.capacity != 0).ToList(); 
    }

    public async Task<List<WithdrawnStudentEntity>> getWithdrawnStudentList()
    {
        return await daoFactory.withdrawnStudentDao!.getAllForCurrentSchoolyear();
    }
}