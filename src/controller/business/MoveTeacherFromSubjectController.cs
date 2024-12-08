using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class MoveTeacherFromSubjectController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<List<TeacherEntity>> getTeacherList()
    {
        return await daoFactory.teacherDao!.getAll();
    }
    
    public async Task<TeacherEntity?> getTeacherById(string teacherId)
    {
        return await daoFactory.teacherDao!.getById(teacherId);
    }
    
    public Task updateTeacherEnrollment(string teacherId)
    {
        throw new NotImplementedException();
    }
}