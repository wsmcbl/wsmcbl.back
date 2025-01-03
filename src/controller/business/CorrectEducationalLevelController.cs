using wsmcbl.src.exception;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class CorrectEducationalLevelController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task changeEducationalLevel(string studentId, int level)
    {
        var student = await daoFactory.academyStudentDao!.getById(studentId);

        if (student == null)
        {
            throw new EntityNotFoundException("student", studentId);
        }
    }
}