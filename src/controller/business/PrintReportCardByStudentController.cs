using wsmcbl.src.controller.service.document;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class PrintReportCardByStudentController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<StudentEntity> getStudentWithGrades(string studentId)
    {
        var student = await daoFactory.academyStudentDao!.getCurrentById(studentId);
        
        var partials = await daoFactory.partialDao!.getListForCurrentSchoolyear();
        student.setPartials(partials);

        return student;
    }

    public async Task<byte[]> getReportCard(string studentId, string userId)
    {
        var documentMaker = new DocumentMaker(daoFactory);
        return await documentMaker.getReportCardByStudent(studentId, userId);
    }

    public async Task<bool> isStudentSolvent(string studentId)
    {
        var debtHistoryList = await daoFactory.debtHistoryDao!.getListByStudentId(studentId);
        
        var debt = debtHistoryList.FirstOrDefault(e => e.isCurrentTariffMonthly());
        return debt != null && debt.isPaid;
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