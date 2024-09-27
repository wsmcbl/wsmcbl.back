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
        var debt = debtHistoryList.First(isSolvencyInterval);
        return debt.isPaid;
    }

    private static bool isSolvencyInterval(DebtHistoryEntity debt)
    {
        var currentMonth = DateTime.Today.Month;
        return debt.tariff.type != 1 && debt.tariff.dueDate!.Value.Month != currentMonth;
    }

    public async Task<TeacherEntity> getTeacherByEnrollment(string enrollmentId)
    {
        return await daoFactory.teacherDao!.getByEnrollmentId(enrollmentId);
    }
}