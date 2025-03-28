using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

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

    public async Task<object?> getSummaryTeacherGrades()
    {
        await Task.CompletedTask;
        return 0;
    }

    public async Task getSummaryRevenue()
    {
        await Task.CompletedTask;
    }
}