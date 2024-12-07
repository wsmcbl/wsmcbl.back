using wsmcbl.src.exception;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class ViewGradeOnlineController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<bool> validateStudent(string studentId, string token)
    {
        var student = await daoFactory.studentDao!.getById(studentId);
        if (student == null)
        {
            throw new EntityNotFoundException("Student", studentId);
        }

        return student.accessToken == token;
    }
}