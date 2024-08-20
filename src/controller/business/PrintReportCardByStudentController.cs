using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class PrintReportCardByStudentController(DaoFactory daoFactory)
    : BaseController(daoFactory), IPrintReportCardByStudentController
{
    public async Task<StudentEntity> getStudentScoreInformation(string studentId)
    {
        var result = await daoFactory.academyStudentDao!.getByIdInCurrentSchoolyear(studentId);
        var partials = await daoFactory.partialDao!.getListByCurrentSchoolyear();
        result.setPartials(partials);
        
        return result;
    }

    public async Task<byte[]> getReportCard(string studentId, (int, int, int) data)
    {
        var printController = new PrintDocumentController(daoFactory);
        return await printController.getReportCardByStudent(studentId, data);
    }

    public async Task<TeacherEntity> getTeacherByEnrollment(string enrollmentId)
    {
        return await daoFactory.teacherDao!.getByEnrollmentId(enrollmentId);
    }
}