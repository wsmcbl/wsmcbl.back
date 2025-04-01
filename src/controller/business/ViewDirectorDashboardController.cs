using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;
using SubjectEntity = wsmcbl.src.model.secretary.SubjectEntity;

namespace wsmcbl.src.controller.business;

public class ViewDirectorDashboardController(DaoFactory daoFactory) : BaseController(daoFactory)
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

    public async Task getSummaryRevenue()
    {
        await Task.CompletedTask;
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

    public async Task<byte[]> getGradeSummaryByEnrollmentId(string enrollmentId, int partialId, string userId)
    {
        await Task.CompletedTask;
        return [];
    }
}