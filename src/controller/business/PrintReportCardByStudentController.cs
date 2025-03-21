using wsmcbl.src.controller.service.document;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;
using StudentEntity = wsmcbl.src.model.academy.StudentEntity;

namespace wsmcbl.src.controller.business;

public class PrintReportCardByStudentController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<StudentEntity> getStudentGradesInformation(string studentId)
    {
        var student = await daoFactory.academyStudentDao!.getCurrentById(studentId);
        var partials = await daoFactory.partialDao!.getListInCurrentSchoolyear();
        student.setPartials(partials);

        return student;
    }

    public async Task<byte[]> getReportCard(string studentId)
    {
        var documentMaker = new DocumentMaker(daoFactory);
        return await documentMaker.getReportCardByStudent(studentId);
    }

    public async Task<bool> isTheStudentSolvent(string studentId)
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