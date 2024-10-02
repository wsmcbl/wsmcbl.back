using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;
using StudentEntity = wsmcbl.src.model.academy.StudentEntity;

namespace wsmcbl.src.controller.business;

public class PrintReportCardByStudentController(DaoFactory daoFactory)
    : BaseController(daoFactory), IPrintReportCardByStudentController
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
        var printController = new PrintDocumentController(daoFactory);
        return await printController.getReportCardByStudent(studentId);
    }

    public async Task<bool> getStudentSolvency(string studentId)
    {
        var debtHistoryList = await daoFactory.debtHistoryDao!.getListByStudent(studentId);
        var debt = debtHistoryList.Find(isSolvencyInterval);

        if (debt == null)
        {
            throw new EntityNotFoundException("No solvency.");
        }
        
        return debt.isPaid;
    }

    private const int MONTHLY_PAYMENT = 1;
    private static bool isSolvencyInterval(DebtHistoryEntity debt)
    {
        var currentMonth = DateTime.Today.Month;
        return debt.tariff.type == MONTHLY_PAYMENT && debt.tariff.checkDueMonth(currentMonth);
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