using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class UnenrollStudentController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task unenrollStudent(string studentId)
    {
        var student = await daoFactory.academyStudentDao!.getCurrentById(studentId);
        await daoFactory.academyStudentDao.deleteAsync(student);
    }
}