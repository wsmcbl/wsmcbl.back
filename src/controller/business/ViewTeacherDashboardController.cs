using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class ViewTeacherDashboardController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<List<SubjectEntity>> getSubjectList(string teacherId)
    {
        return await daoFactory.academySubjectDao!.getListByTeacherId(teacherId);
    }
}