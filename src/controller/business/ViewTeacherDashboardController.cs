using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class ViewTeacherDashboardController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<List<SubjectEntity>> getSubjectList(string teacherId)
    {
        return await daoFactory.academySubjectDao!.getListByTeacherId(teacherId);
    }

    public async Task<List<SubjectPartialEntity>> getSubjectListByGrade(string teacherId)
    {
        var partialList = await daoFactory.partialDao!.getListForCurrentSchoolyear();
        
        var firstPartial = partialList.FirstOrDefault(e => e is { semester: 1, partial: 1 });
        if (firstPartial == null)
        {
            return [];
        }
        
        return await daoFactory.subjectPartialDao!.getSubjectList(teacherId, firstPartial.partialId);
    }
}