using wsmcbl.src.controller.service;
using wsmcbl.src.exception;
using wsmcbl.src.model;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;
using StudentEntity = wsmcbl.src.model.academy.StudentEntity;

namespace wsmcbl.src.controller.business;

public class PrintReportCardByStudentController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<StudentEntity> getStudentGradesInformation(string studentId)
    {
        var student = await daoFactory.academyStudentDao!.getByIdInCurrentSchoolyear(studentId);
        var partials = await daoFactory.partialDao!.getListByCurrentSchoolyear();
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
        var debtHistoryList = await daoFactory.debtHistoryDao!.getListByStudent(studentId);
        
        var debt = debtHistoryList.Find(isSolvencyInterval);
        if (debt == null)
        {
            throw new EntityNotFoundException("Monthly tariff not found.");
        }
        
        return debt.isPaid;
    }

    private static bool isSolvencyInterval(DebtHistoryEntity debt)
    {
        var currentMonth = DateTime.Today.Month;
        return debt.tariff.type == Const.TARIFF_MONTHLY && debt.tariff.checkDueMonth(currentMonth);
    }

    public async Task<TeacherEntity> getTeacherByEnrollment(string enrollmentId)
    {
        var result = await daoFactory.teacherDao!.getByEnrollmentId(enrollmentId);
        if (result == null)
        {
            throw new EntityNotFoundException($"Teacher with enrollmentId ({enrollmentId}) not found.");
        }

        return result;
    }
}