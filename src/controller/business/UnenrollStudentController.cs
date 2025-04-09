using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class UnenrollStudentController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<List<WithdrawnStudentEntity>> getWithdrawnStudentList()
    {
        return await daoFactory.withdrawnStudentDao!.getAll();
    }
    
    public async Task unenrollStudent(string studentId)
    {
        var student = await daoFactory.academyStudentDao!.getCurrentById(studentId);
        daoFactory.academyStudentDao.delete(student);

        var withdrawnStudent = new WithdrawnStudentEntity(student);
        daoFactory.withdrawnStudentDao!.create(withdrawnStudent);
        
        await daoFactory.ExecuteAsync();
        
        var controller = new UpdateStudentProfileController(daoFactory);
        await controller.updateProfileState(studentId, false);
    }
}