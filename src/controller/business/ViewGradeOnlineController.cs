using wsmcbl.src.controller.service.document;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class ViewGradeOnlineController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<bool> isStudentSolvent(string studentId)
    {
        var controller = new PrintReportCardByStudentController(daoFactory);
        return await controller.isStudentSolvent(studentId);
    }

    public async Task<bool> tokenIsNotValid(string studentId, string token)
    {
        var student = await daoFactory.studentDao!.getById(studentId);
        if (student == null)
        {
            return false;
        }

        return student.accessToken != token;
    }

    public async Task<byte[]> getGradeReport(string studentId)
    {
        var documentMaker = new DocumentMaker(daoFactory);
        return await documentMaker.getReportCardByStudent(studentId, null);
    }

    public async Task<StudentEntity> getStudent(string studentId)
    {
        return await daoFactory.academyStudentDao!.getCurrentById(studentId);
    }

    public async Task<TeacherEntity> getTeacherByEnrollment(string enrollmentId)
    {
        var result = await daoFactory.teacherDao!.getByEnrollmentId(enrollmentId);
        if (result == null)
        {
            throw new EntityNotFoundException($"Teacher with enrollmentId ({enrollmentId}) not found.");
        }

        await result.setCurrentEnrollment(daoFactory.schoolyearDao!);
        
        return result;
    }
}