using wsmcbl.src.exception;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class ViewGradeOnlineController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<bool> isTheStudentSolvent(string studentId)
    {
        var controller = new PrintReportCardByStudentController(daoFactory);
        return await controller.isTheStudentSolvent(studentId);
    }

    public async Task<byte[]> getGradeReport(string studentId)
    {
        var student = await daoFactory.academyStudentDao!.getById(studentId);
        return [];
    }

    public async Task<bool> isTokenCorrect(string studentId, string token)
    {
        var student = await daoFactory.studentDao!.getById(studentId);
        if (student == null)
        {
            throw new EntityNotFoundException("Student", studentId);
        }

        return student.accessToken == token;
    }
}